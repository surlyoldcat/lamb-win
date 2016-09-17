using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lamb.model
{
    public class LambInvocationResult : OperationResult
    {        
        public InvokeErrorInfo Error { get; set; }
        public string ReturnPayload { get; set; }

        public override string Format()
        {
            StringBuilder sb = new StringBuilder(base.Format());
            throw new NotImplementedException();
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
                Message = message,
                Error = new InvokeErrorInfo
                {
                    CaughtException = ex,
                    ErrorMessage = ex.Message
                }
            };
        }

        public static LambInvocationResult Fail(string message)
        {
            return Fail(null, message);
        }
    }

    public class InvokeErrorInfo : IInvokeErrorInfo
    {
        public string ErrorMessage { get; set; }
        public Exception CaughtException { get; set; }
    }
}
