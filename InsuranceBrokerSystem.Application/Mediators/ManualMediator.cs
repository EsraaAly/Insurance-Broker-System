using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceBrokerSystem.Application.Mediators
{
    public class ManualMediator : IManualMediator
    {
        private readonly IServiceProvider _serviceProvider;

        public ManualMediator(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task<TResponse> Send<TResponse>(IManualRequest<TResponse> request, CancellationToken cancellationToken = default)
        {
            var handlerType = typeof(IManualRequestHandler<,>).MakeGenericType(request.GetType(), typeof(TResponse));

            var handler = _serviceProvider.GetService(handlerType);

            if (handler == null)
            {
                throw new InvalidOperationException($"No handler found for request of type {request.GetType().Name}");
            }

            var method = handlerType.GetMethod("ManualHandle");
            if (method == null)
            {
                throw new InvalidOperationException($"Handler for request of type {request.GetType().Name} does not contain a ManualHandle method");
            }

            var task = (Task<TResponse>)method.Invoke(handler, new object[] { request, cancellationToken });

             return await task;
        }
    }
}
