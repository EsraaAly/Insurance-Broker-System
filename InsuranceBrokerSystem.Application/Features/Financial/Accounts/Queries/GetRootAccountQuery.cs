using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceBrokerSystem.Application.Features.Financial.Accounts.Queries
{
    public record GetRootAccountQuery:IRequest<Result<List<GetAccountDTO>>>;

    public class GetRootAccountHandler : IRequestHandler<GetRootAccountQuery, Result<List<GetAccountDTO>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetRootAccountHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Result<List<GetAccountDTO>>> Handle(GetRootAccountQuery request, CancellationToken cancellationToken)
        {
            var accounts = await _unitOfWork.AccountNumberRepository.GetAllEntitytiesAsync();
            if (accounts == null || accounts.Count == 0)
            {
                return Result<List<GetAccountDTO>>.Failure("No accounts found.");
            }
            var rootAccounts = accounts.Where(a => a.ParentId == 1).ToList();

            return Result<List<GetAccountDTO>>.Success(_mapper.Map<List<GetAccountDTO>>(rootAccounts));
        }
    }
}
