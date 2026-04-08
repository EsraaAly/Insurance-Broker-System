using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceBrokerSystem.Application.Features.Clients.Queries
{
    public record GetAllClientsQuery : IRequest<Result<List<GetClientDTO>>>;

    public class GetAllClientsHandler : IRequestHandler<GetAllClientsQuery, Result<List<GetClientDTO>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetAllClientsHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<List<GetClientDTO>>> Handle(GetAllClientsQuery request, CancellationToken cancellationToken)
        {
            var entries = await _unitOfWork.ClientRepository.GetAllEntitytiesAsync();
            if (entries == null )
            {
                return Result<List<GetClientDTO>>.Failure("Could not retrieve clients from the server");
            }

            List<GetClientDTO> dto = entries.Adapt<List<GetClientDTO>>();
            return Result<List<GetClientDTO>>.Success(dto);
        }
    }    
} 
