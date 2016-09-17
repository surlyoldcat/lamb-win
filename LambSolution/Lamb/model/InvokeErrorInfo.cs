using System;

namespace Lamb.model
{
    public class IInvokeErrorInfo
    {
        string ErrorMessage { get; set; }
        Exception CaughtException { get; set; }
    }
}