using System;

namespace Lamb.service
{
    public interface IInvokeErrorInfo
    {
        string ErrorMessage { get; set; }
        Exception CaughtException { get; set; }
    }
}