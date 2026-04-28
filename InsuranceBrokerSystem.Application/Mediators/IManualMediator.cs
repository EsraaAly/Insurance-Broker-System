using Azure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceBrokerSystem.Application.Mediators
{
    public interface IManualMediator
    {
        Task<TResponse> Send<TResponse>(IManualRequest<TResponse> request, CancellationToken cancellationToken = default);
    }
}
