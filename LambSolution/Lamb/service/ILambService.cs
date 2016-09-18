using System.Threading.Tasks;
using Lamb.model;

namespace Lamb.service
{
    public interface ILambService
    {
        Task<LambdaListResult> ListAllLambdas();
        Task<LambInvocationResult> Execute(LambdaInvokeInfo info);
    }
}