using InsuranceBrokerSystem.Application.DTOs.Master_Table.SourceOfIncome;

namespace InsuranceBrokerSystem.Application.Features.SourceOfIncomes.Queries.GetSourceOfIncomeById
{
    public class GetSourceOfIncomeByIdQuery : IRequest<Result<GetSourceOfIncomeDTO>>
    {
        public int Id { get; set; }
    }

    public class GetSourceOfIncomeByIdHandler : IRequestHandler<GetSourceOfIncomeByIdQuery, Result<GetSourceOfIncomeDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetSourceOfIncomeByIdHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<GetSourceOfIncomeDTO>> Handle(GetSourceOfIncomeByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _unitOfWork.GSourceOfIncome.GetEntityByIdAsync(request.Id);
            if (entity == null)
            {
                return Result<GetSourceOfIncomeDTO>.Failure("Source Of Income not found");
            }

            var dto = new GetSourceOfIncomeDTO
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description
            };

            return Result<GetSourceOfIncomeDTO>.Success(dto, "Source Of Income retrieved successfully");
        }
    }

    
}
