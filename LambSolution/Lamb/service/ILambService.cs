using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon;
using Amazon.Runtime;
using Lamb.model;

namespace Lamb.service
{

    public interface ILambService
    {
        Task<LambdaListResult> ListAllLambdas();
        Task<LambInvocationResult> Execute(LambdaInvokeInfo info);
    }
}
