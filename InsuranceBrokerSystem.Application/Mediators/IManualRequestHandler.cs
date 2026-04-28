using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceBrokerSystem.Application.Mediators
{
    public interface IManualRequestHandler<TRequest,TRespone> where TRequest : IManualRequest<TRespone>
    {
        Task<TRespone> ManualHandle(TRequest request, CancellationToken cancellationToken);
    }
}
