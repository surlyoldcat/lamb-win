using System;
using System.Text;
using Lamb.service;

namespace Lamb.model
{
    public class LambInvocationResult : OperationResult
    {
        public InvokeErrorInfo Error { get; set; }
        public string ReturnPayload { get; set; }

        public override string Format()
        {
            StringBuilder msg = new StringBuilder();
            if (Success)
            {
                msg.AppendLine("Lambda invoked successfully.");
                msg.AppendLine("Returned data:");
                if (String.IsNullOrEmpty(ReturnPayload))
                {
                    msg.AppendLine(Constants.NULL_STRING);
                }
                else
                {
                    msg.AppendLine(ReturnPayload.PrettyPrintJson());
                }
            }
            else
            {
                msg.AppendLine("Lambda invocation failed!");
                msg.AppendLine("Error info:");
                msg.AppendFormat("Error message: {0}{1}", Error.ErrorMessage, Environment.NewLine);
                msg.AppendLine("Exception: ");
                msg.AppendLine(Error.ExceptionDump);
            }

            //msg.AppendLine();
            return msg.ToString();
        }


        public static LambInvocationResult Pass(string returnedPayload)
        {
            return new LambInvocationResult
            {
                Success = true,
                ReturnPayload = returnedPayload
            };
        }

        public static LambInvocationResult Fail(Exception ex, string message)
        {
            return new LambInvocationResult
            {
                Success = false,
                Error = new InvokeErrorInfo
                {
                    ExceptionDump = ex.ToString(),
                    ErrorMessage = message
                }
            };
        }

        public new static LambInvocationResult Fail(string message)
        {
            return new LambInvocationResult
            {
                Success = false,
                Error = new InvokeErrorInfo
                {
                    ErrorMessage = message,
                    ExceptionDump = Constants.NULL_STRING
                }
            };
        }
    }

    public class InvokeErrorInfo : IInvokeErrorInfo
    {
        public string ErrorMessage { get; set; }
        public string ExceptionDump { get; set; }
    }
}