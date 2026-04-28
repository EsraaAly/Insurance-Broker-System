using InsuranceBrokerSystem.Application.DTOs.Master_Table.Bank;
using MediatR;
using InsuranceBrokerSystem.Application.Common;
using InsuranceBrokerSystem.Application.Mediators;

namespace InsuranceBrokerSystem.Application.Features.Banks.Queries.GetAllBanks
{
    public class GetAllBanksQuery : IManualRequest<Result<List<GetBankDTO>>>
    {
    }

    public class GetAllBanksHandler : IManualRequestHandler<GetAllBanksQuery, Result<List<GetBankDTO>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllBanksHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<List<GetBankDTO>>> ManualHandle(GetAllBanksQuery request, CancellationToken cancellationToken)
        {
            var banks = await _unitOfWork.GBank.GetAllEntitytiesAsync();
            var dtos = banks.Adapt<List<GetBankDTO>>();
            
            return Result<List<GetBankDTO>>.Success(dtos, "Banks retrieved successfully");
        }
    }
}
