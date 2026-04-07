
namespace InsuranceBrokerSystem.Application.Services.Master_Table
{
    public class InsuranceLOBService : IInsuranceLOBService
    {
        private readonly IUnitOfWork _unitOfWork;

        public InsuranceLOBService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<bool>> AddLOBAsync(AddInsuranceLOBDTO insuranceLine)
        {
            if (insuranceLine != null)
            {
                var Insureanceclass = await _unitOfWork.GInsuranceClass.GetEntityByIdAsync(insuranceLine.ClassID);
                var insuranceLine1 = new Domain.Entities.Master_Table.InsuranceLineOfBusiness
                {
                    ClassID = insuranceLine.ClassID,
                    InsuranceClass = Insureanceclass,
                    LineOfBusiness = insuranceLine.LineOfBusiness,
                    Abbreviation = insuranceLine.Abbreviation,
                    CreatedBy = "Israa",
                    CreatedDate = DateTime.Now,
                    UpdatedBy = "",
                    UpdatedDate = DateTime.Now,
                    IsDeleted = false,
                };
                var entity = await _unitOfWork.InsuranceLOBRepository.AddEntityAsync(insuranceLine1);
                if (entity != null)
                {
                    await _unitOfWork.CommitAsync();
                    return Result<bool>.Success(true, "Line of Business added successfully");
                }
            }
            return Result<bool>.Failure("Failed to add Line of Business");
        }

        public async Task<Result<bool>> DeleteLOBAsync(int id)
        {
            var insuranceLine = await _unitOfWork.InsuranceLOBRepository.GetEntityByIdAsync(id);

            if (insuranceLine != null)
            {
                var success = await _unitOfWork.InsuranceLOBRepository.DeleteEntityAsync(id);
                if (success)
                {
                    await _unitOfWork.CommitAsync();
                    return Result<bool>.Success(true, "Line of Business deleted successfully");
                }
                return Result<bool>.Failure("Failed to delete Line of Business");
            }
            return Result<bool>.Failure("Line of Business not found");
        }

        public async Task<Result<List<GetInsuranceLOBDTO>>> GetAllLOBsAsync()
        {
            var masterClasses = await _unitOfWork.GInsuranceClass.GetAllEntitytiesAsync();
            var entities = await _unitOfWork.InsuranceLOBRepository.GetAllEntitytiesAsync();
            
            if (entities == null)
            {
                return Result<List<GetInsuranceLOBDTO>>.Failure("No Lines of Business found");
            }

            List<GetInsuranceLOBDTO> dtos = entities.Select(i => new GetInsuranceLOBDTO
            {
                Id = i.Id,
                LineOfBusiness = i.LineOfBusiness,
                Abbreviation = i.Abbreviation,
                ClassID = i.ClassID,
                ClassName = masterClasses?.FirstOrDefault(c => c.Id == i.ClassID)?.ClassName ?? "N/A",
                CreatedBy = i.CreatedBy,
                CreatedDate = i.CreatedDate,
                UpdatedBy = i.UpdatedBy,
                UpdatedDate = i.UpdatedDate,
            }).ToList();

            return Result<List<GetInsuranceLOBDTO>>.Success(dtos);
        }

        public async Task<Result<List<GetInsuranceLOBDTO>>> GetInsuranceLOBByClassIdAsync(int classId)
        {
            var LOB_List = await _unitOfWork.InsuranceLOBRepository.GetInsuranceLOBByClassIdAsync(classId);
            if (LOB_List == null)
            {
                return Result<List<GetInsuranceLOBDTO>>.Failure("No Lines of Business found for this class");
            }
            List<GetInsuranceLOBDTO> dto = LOB_List.Adapt<List<GetInsuranceLOBDTO>>();
            return Result<List<GetInsuranceLOBDTO>>.Success(dto);
        }

        public async Task<Result<GetInsuranceLOBDTO>> GetLOBByidAsync(int id)
        {
            var entity = await _unitOfWork.InsuranceLOBRepository.GetEntityByIdAsync(id);

            if (entity == null)
            {
                return Result<GetInsuranceLOBDTO>.Failure("Line of Business not found");
            }

            GetInsuranceLOBDTO dto = new GetInsuranceLOBDTO
            {
                Id = entity.Id,
                LineOfBusiness = entity.LineOfBusiness,
                Abbreviation = entity.Abbreviation,
                CreatedBy = entity.CreatedBy,
                CreatedDate = entity.CreatedDate,
                UpdatedBy = entity.UpdatedBy,
                UpdatedDate = entity.UpdatedDate
            };

            return Result<GetInsuranceLOBDTO>.Success(dto);
        }

        public async Task<Result<GetInsuranceLOBDTO>> UpdateLOBAsync(UpdateInsuranceLOBDTO dto)
        {
            var Insureanceclass = await _unitOfWork.GInsuranceClass.GetEntityByIdAsync(dto.ClassID);
            var entity = await _unitOfWork.InsuranceLOBRepository.GetEntityByIdAsync(dto.Id);

            if (entity == null)
            {
                return Result<GetInsuranceLOBDTO>.Failure("Line of Business not found");
            }

            entity.LineOfBusiness = dto.LineOfBusiness;
            entity.Abbreviation = dto.Abbreviation;
            entity.ClassID = dto.ClassID;
            entity.InsuranceClass = Insureanceclass;

            entity.UpdatedBy = "Israa";
            entity.UpdatedDate = DateTime.Now;

            bool isUpdated = await _unitOfWork.InsuranceLOBRepository.UpdateEntityAsync(entity);

            if (isUpdated)
            {
                await _unitOfWork.CommitAsync();
                return Result<GetInsuranceLOBDTO>.Success(new GetInsuranceLOBDTO
                {
                    Id = entity.Id,
                    LineOfBusiness = entity.LineOfBusiness,
                    Abbreviation = entity.Abbreviation,
                    CreatedBy = entity.CreatedBy,
                    CreatedDate = entity.CreatedDate,
                    UpdatedBy = entity.UpdatedBy,
                    UpdatedDate = entity.UpdatedDate
                }, "Line of Business updated successfully");
            }

            return Result<GetInsuranceLOBDTO>.Failure("Failed to update Line of Business");
        }

    }
}
