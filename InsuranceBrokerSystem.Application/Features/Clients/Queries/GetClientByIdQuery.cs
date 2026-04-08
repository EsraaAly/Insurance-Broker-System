using InsuranceBrokerSystem.Application.DTOs.Client;

namespace InsuranceBrokerSystem.Application.Features.Clients.Queries
{
    public record GetClientByIdQuery(int Id) : IRequest<Result<GetClientDTO>>;

    public class GetClientByIdHandler : IRequestHandler<GetClientByIdQuery, Result<GetClientDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetClientByIdHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<GetClientDTO>> Handle(GetClientByIdQuery request, CancellationToken cancellationToken)
        {
            var client = await _unitOfWork.ClientRepository.GetEntityByIdAsync(request.Id);
            if (client == null)
            {
                return Result<GetClientDTO>.Failure("Client not found");
            }

            GetClientDTO clientDTO = client.Adapt<GetClientDTO>();
            return Result<GetClientDTO>.Success(clientDTO);
        }
    }
}
