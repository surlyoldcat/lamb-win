using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Amazon;
using Amazon.Lambda;
using Amazon.Lambda.Model;
using Amazon.Runtime;
using Lamb.model;

namespace Lamb.service
{
    internal class LambService : ILambService
    {
        public LambService(AWSCredentials creds, RegionEndpoint region)
        {
            Credentials = creds;
            Region = region;
        }

        private AWSCredentials Credentials { get; }
        private RegionEndpoint Region { get; }

        public async Task<LambdaListResult> ListAllLambdas()
        {            
            LambdaListResult retval;
            AmazonLambdaClient client = new AmazonLambdaClient(Credentials, Region);
            try
            {
                //undocumented feature, ListFunctions will not return more than 50
                //items without making a request to AWS services team, it is an 
                //account limit. so, have to use pagination to get all Lambdas
                var functionConfigs = await FetchAllFunctionsPaged(client);
                var proj = functionConfigs.Select(f => new LambdaListItem
                {
                    Arn = f.FunctionArn,
                    Name = f.FunctionName
                }).OrderBy(lli => lli.Name);
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


        public static OperationResult VerifyCredentials(AWSCredentials creds, RegionEndpoint region)
        {
            // very simple way to do a basic credential check- see if they
            // can be used to call the ListFunctions API method; 
            OperationResult retval;
            AmazonLambdaClient client = new AmazonLambdaClient(creds, region);
            try
            {
                ListFunctionsRequest request = new ListFunctionsRequest
                {
                    MaxItems = 1
                };
                var response = client.ListFunctions(request);
                response.EnsureSuccessResponse("Lambda:ListFunctions");
                if (null != response.Functions && response.Functions.Any())
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
                client.Dispose();
            }
            return retval;
        }

        private static async Task<List<FunctionConfiguration>> FetchAllFunctionsPaged(AmazonLambdaClient client, ListFunctionsResponse prevResponse = null)
        {
            List<FunctionConfiguration> functions = new List<FunctionConfiguration>(100);
            // if the previous call had a NextMarker, we're in a recursive paged fetch
            var request = new ListFunctionsRequest
            {
                MaxItems = 100,
                Marker = prevResponse?.NextMarker
            };
            var response = await client.ListFunctionsAsync(request);
            response.EnsureSuccessResponse("Lambda:ListFunctions");
            if (null != response.Functions && response.Functions.Any())
            {
                //grab this page of lambdas
                functions = response.Functions;
                if (!String.IsNullOrEmpty(response.NextMarker))
                {
                    //there's more to fetch: recurse
                    functions.AddRange(await FetchAllFunctionsPaged(client, response));
                }
            }
            return functions;
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