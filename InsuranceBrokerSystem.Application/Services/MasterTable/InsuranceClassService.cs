
namespace InsuranceBrokerSystem.Application.Services.Master_Table
{
    public class InsuranceClassService : IInsuranceClassService
    {
        private readonly IUnitOfWork _unitOfWork;

        public InsuranceClassService(IUnitOfWork unitOfWorkrepo)
        {
            _unitOfWork = unitOfWorkrepo;
        }
        public async Task<Result<bool>> AddClassAsync(AddInsuranceClassDTO insuranceClass)
        {
            if (insuranceClass != null)
            {
                var insuranceClass1 = new Domain.Entities.Master_Table.InsuranceClass
                {
                    ClassName = insuranceClass.ClassName,
                    Abbreviation = insuranceClass.Abbreviation,
                    CreatedBy = "Israa",
                    CreatedDate = DateTime.Now,
                    UpdatedBy = "",
                    UpdatedDate = DateTime.Now,
                    IsDeleted = false,
                };
                var entity = await _unitOfWork.GInsuranceClass.AddEntityAsync(insuranceClass1);
                if (entity != null)
                {
                    await _unitOfWork.CommitAsync();
                    return Result<bool>.Success(true, "Insurance Class added successfully");
                }
            }
            return Result<bool>.Failure("Failed to add Insurance Class");
        }

        public async Task<Result<bool>> DeleteClassAsync(int id)
        {
            var insuranceClass = await _unitOfWork.GInsuranceClass.GetEntityByIdAsync(id);

            if (insuranceClass != null)
            {
                var success = await _unitOfWork.GInsuranceClass.DeleteEntityAsync(id);

                if (success)
                {
                    await _unitOfWork.CommitAsync();
                    return Result<bool>.Success(true, "Insurance Class deleted successfully");
                }
                return Result<bool>.Failure("Failed to delete Insurance Class");
            }
            return Result<bool>.Failure("Insurance Class not found");
        }

        public async Task<Result<List<GetInsuranceClassDTO>>> GetAllClassesAsync()
        {
            var entities = await _unitOfWork.GInsuranceClass.GetAllEntitytiesAsync();
            if (entities == null)
            {
                return Result<List<GetInsuranceClassDTO>>.Failure("No insurance classes found");
            }

            var dtos = entities.Select(i => new GetInsuranceClassDTO
            {
                Id = i.Id,
                ClassName = i.ClassName,
                Abbreviation = i.Abbreviation,
                CreatedBy = i.CreatedBy,
                CreatedDate = i.CreatedDate,
                UpdatedBy = i.UpdatedBy,
                UpdatedDate = i.UpdatedDate,
            }).ToList();

            return Result<List<GetInsuranceClassDTO>>.Success(dtos);
        }

        public async Task<Result<GetInsuranceClassDTO>> GetClassByidAsync(int id)
        {
            var entity = await _unitOfWork.GInsuranceClass.GetEntityByIdAsync(id);

            if (entity == null)
            {
                return Result<GetInsuranceClassDTO>.Failure("Insurance Class not found");
            }

            var dto = new GetInsuranceClassDTO
            {
                Id = entity.Id,
                ClassName = entity.ClassName,
                Abbreviation = entity.Abbreviation,
                CreatedBy = entity.CreatedBy,
                CreatedDate = entity.CreatedDate,
                UpdatedBy = entity.UpdatedBy,
                UpdatedDate = entity.UpdatedDate
            };

            return Result<GetInsuranceClassDTO>.Success(dto);
        }

        public async Task<Result<GetInsuranceClassDTO>> UpdateClassAsync(UpdateInsuranceClassDTO dto)
        {
            var entity = await _unitOfWork.GInsuranceClass.GetEntityByIdAsync(dto.Id);

            if (entity == null)
            {
                return Result<GetInsuranceClassDTO>.Failure("Insurance Class not found");
            }

            entity.ClassName = dto.ClassName;
            entity.Abbreviation = dto.Abbreviation;
            entity.UpdatedBy = "Israa";
            entity.UpdatedDate = DateTime.Now;

            bool isUpdated = await _unitOfWork.GInsuranceClass.UpdateEntityAsync(entity);
            if (isUpdated)
            {
                await _unitOfWork.CommitAsync();
                var updatedDto = new GetInsuranceClassDTO
                {
                    Id = entity.Id,
                    ClassName = entity.ClassName,
                    Abbreviation = entity.Abbreviation,
                    CreatedBy = entity.CreatedBy,
                    CreatedDate = entity.CreatedDate,
                    UpdatedBy = entity.UpdatedBy,
                    UpdatedDate = entity.UpdatedDate
                };
                return Result<GetInsuranceClassDTO>.Success(updatedDto, "Insurance Class updated successfully");
            }

            return Result<GetInsuranceClassDTO>.Failure("Failed to update Insurance Class");
        }
    }
}
