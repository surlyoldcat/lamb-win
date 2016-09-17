using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon;
using Amazon.IdentityManagement;
using Amazon.IdentityManagement.Model;
using Amazon.Lambda;
using Amazon.Lambda.Model;
using Amazon.Runtime;
using Lamb.model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Lamb.service
{
    internal class LambService : ILambService
    {
        private AWSCredentials Credentials { get; set; }
        private RegionEndpoint Region { get; set; }

        public LambService(AWSCredentials creds, RegionEndpoint region)
        {
            Credentials = creds;
            Region = region;
        }

        public async Task<LambdaListResult> ListAllLambdas()
        {
            LambdaListResult retval;
            AmazonLambdaClient client = new AmazonLambdaClient(Credentials, Region);
            try
            {
                var response = client.ListFunctions();
                response.EnsureSuccessResponse("Lambda:ListFunctions");
                var proj = response.Functions.Select(f => new LambdaListItem
                {
                    Arn = f.FunctionArn,
                    Name = f.FunctionName
                });
                retval = LambdaListResult.Pass(proj.ToList());
            }
            catch (Exception ex)
            {
                retval = LambdaListResult.Fail(ex, "Failed to fetch Lambda list!");
            }
            finally
            {
                client.Dispose();
            }
            
            return retval;

        }

        public async Task<LambInvocationResult> Execute(LambdaInvokeInfo info)
        {
            LambInvocationResult retval;
            AmazonLambdaClient client = new AmazonLambdaClient(Credentials, Region);
            InvokeRequest request = new InvokeRequest
            {
                FunctionName = info.Name,
                Payload = info.FunctionPayload
            };
            try
            {
                var response = await client.InvokeAsync(request);
                response.EnsureSuccessResponse("Lambda:Invoke");
                string returnPayload = ReadPayloadStream(response.Payload);
                bool functionSucceeded = String.IsNullOrEmpty(response.FunctionError);
                if (functionSucceeded)
                {
                    retval = LambInvocationResult.Pass(returnPayload);
                }
                else
                {
                    retval = LambInvocationResult.Fail(returnPayload);
                }
            }
            catch (Exception ex)
            {
                retval = LambInvocationResult.Fail(ex, "API call failed!");
            }
            finally
            {
                client.Dispose();
            }
            return retval;
        }



        public static OperationResult VerifyCredentials(AWSCredentials creds)
        {
            OperationResult retval;
            AmazonIdentityManagementServiceClient iam = new AmazonIdentityManagementServiceClient(creds);
            try
            {
                var response = iam.GetUser();
                response.EnsureSuccessResponse("IAM:GetUser");
                if (null != response.User && !String.IsNullOrEmpty(response.User.Arn))
                {
                    retval = OperationResult.Pass();
                }
                else
                {
                    retval = OperationResult.Fail("Unable to verify credentials.");
                }
            }
            catch (Exception ex)
            {
                string failMsg = String.Format("Failed to verify credentials: {0}", ex.Message);
                retval = OperationResult.Fail(failMsg);
            }
            finally
            {
                iam.Dispose();
            }
            return retval;
        }

        private static string ReadPayloadStream(MemoryStream payload)
        {
            using (StreamReader rdr = new StreamReader(payload))
            {
                return rdr.ReadToEnd();
            }
        }
    }
}
