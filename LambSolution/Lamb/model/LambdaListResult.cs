using System;
using System.Collections.Generic;

namespace Lamb.model
{
    public class LambdaListResult : OperationResult
    {
        public List<LambdaListItem> Lambdas { get; set; }
        public Exception ThrownException { get; set; }

        public static LambdaListResult Pass(List<LambdaListItem> results)
        {
            return new LambdaListResult
            {
                Success = true,
                Lambdas = results
            };
        }

        public static LambdaListResult Fail(Exception ex, string message)
        {
            return new LambdaListResult
            {
                Success = false,
                Message = message,
                ThrownException = ex
            };
        }
    }
}