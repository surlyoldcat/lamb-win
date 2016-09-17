using System.Collections.Generic;

namespace Lamb.service
{
    public interface ILambInvocationResult
    {
        bool InvokeSuccess { get; set; }
        string ResponseMessage { get; set; }
        List<InvokeErrorInfo> Errors { get; set; }
        string Format();
    }
}