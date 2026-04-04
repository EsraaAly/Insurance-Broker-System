
namespace InsuranceBrokerSystem.Application.Services.Master_Table
{
    public class InsuranceClassService : IInsuranceClassService
    {
        private readonly IUnitOfWork _unitOfWork;

        public InsuranceClassService(IUnitOfWork unitOfWorkrepo)
        {
            _unitOfWork = unitOfWorkrepo;
        }
        public async Task<bool> AddClassAsync(AddInsuranceClassDTO insuranceClass)
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
                    UpdatedDate =DateTime.Now,
                    IsDeleted = false,
                };
                var entity = await _unitOfWork.GInsuranceClass.AddEntityAsync(insuranceClass1);
                if (entity != null)
                {
                   await _unitOfWork.CommitAsync();
                    return true;
                }
               

            }
            return false;
        }

        public async Task<bool> DeleteClassAsync(int id)
        {
            var insuranceClass = _unitOfWork.GInsuranceClass.GetEntityByIdAsync(id);

            if (insuranceClass != null)
            {
               var success =  await _unitOfWork.GInsuranceClass.DeleteEntityAsync(id);
                
                if (success)
                {
                    await _unitOfWork.CommitAsync();
                    return true;
                }
                return false;
            }
            return false;
        }

        public async Task<List<GetInsuranceClassDTO>> GetAllClassesAsync()
        {
            var insuranceClasses = await _unitOfWork.GInsuranceClass.GetAllEntitytiesAsync().ContinueWith(t =>
            {
                if (t.Result != null)
                {
                    List<GetInsuranceClassDTO> insuranceClasses = t.Result.Select(i => new GetInsuranceClassDTO
                    {
                        Id = i.Id,
                        ClassName = i.ClassName,
                        Abbreviation = i.Abbreviation,
                        CreatedBy = i.CreatedBy,
                        CreatedDate = i.CreatedDate,
                        UpdatedBy = i.UpdatedBy,
                        UpdatedDate = i.UpdatedDate,
                    }).ToList();
                    return insuranceClasses;
                }
                return null;
            });
            return insuranceClasses;
        }

        public async Task<GetInsuranceClassDTO> GetClassByidAsync(int id)
        {
            var entity = await _unitOfWork.GInsuranceClass.GetEntityByIdAsync(id);

            if (entity == null) return null;

            // 3. Map the data to the DTO
            GetInsuranceClassDTO dto = new GetInsuranceClassDTO
            {
                Id = entity.Id,
                ClassName = entity.ClassName,
                Abbreviation = entity.Abbreviation,
                CreatedBy = entity.CreatedBy,
                CreatedDate = entity.CreatedDate,
                UpdatedBy = entity.UpdatedBy,
                UpdatedDate = entity.UpdatedDate
            };

            return dto;
        }

        public async Task<GetInsuranceClassDTO> UpdateClassAsync(UpdateInsuranceClassDTO dto)
        {
            // 1. Fetch the EXISTING record from the repository
            var entity = await _unitOfWork.GInsuranceClass.GetEntityByIdAsync(dto.Id);

            if (entity == null) return null;

            // 2. UPDATE the existing entity properties with data from the DTO
            // Do NOT create a 'new' entity; modify the one we just fetched
            entity.ClassName = dto.ClassName;
            entity.Abbreviation = dto.Abbreviation;

            // Audit fields
            entity.UpdatedBy = "Israa";
            entity.UpdatedDate = DateTime.Now;

            // 3. Save the changes via the repository
            bool isUpdated = await _unitOfWork.GInsuranceClass.UpdateEntityAsync(entity);
            await _unitOfWork.CommitAsync();
            if (isUpdated)
            {
                // 4. Map the updated entity back to a GetInsuranceClassDTO for the UI
                return new GetInsuranceClassDTO
                {
                    Id = entity.Id,
                    ClassName = entity.ClassName,
                    Abbreviation = entity.Abbreviation,
                    CreatedBy = entity.CreatedBy,
                    CreatedDate = entity.CreatedDate,
                    UpdatedBy = entity.UpdatedBy,
                    UpdatedDate = entity.UpdatedDate
                };
            }

            return null;
        }
    }
}
