namespace InsuranceBrokerSystem.Application.Features.Nationalities.Queries.GetNationalityById
{
    public class GetNationalityByIdQuery : IRequest<Result<GetNationalityDTO>>
    {
        public int Id { get; set; }
    }

    public class GetNationalityByIdHandler : IRequestHandler<GetNationalityByIdQuery, Result<GetNationalityDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetNationalityByIdHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<GetNationalityDTO>> Handle(GetNationalityByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _unitOfWork.GNationality.GetEntityByIdAsync(request.Id);
            if (entity == null)
            {
                return Result<GetNationalityDTO>.Failure("Nationality not found");
            }

            var dto = entity.Adapt<GetNationalityDTO>();

            return Result<GetNationalityDTO>.Success(dto, "Nationality retrieved successfully");
        }
    }
    class getNationalityByIdValidator : AbstractValidator<GetNationalityByIdQuery>
    {
        public getNationalityByIdValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("Id must be greater than 0");
        }
    }
}
