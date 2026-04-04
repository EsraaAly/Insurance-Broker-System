
namespace InsuranceBrokerSystem.Application.Services.Master_Table
{
    public class InsuranceLOBService : IInsuranceLOBService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public InsuranceLOBService(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;

        }
        public async Task<bool> AddLOBAsync(AddInsuranceLOBDTO insuranceLine)
        {
            if (insuranceLine != null)
            {
                var Insureanceclass  = await _unitOfWork.GInsuranceClass.GetEntityByIdAsync(insuranceLine.ClassID);
                var insuranceLine1 = new Domain.Entities.Master_Table.InsuranceLineOfBusiness
                {
                    ClassID = insuranceLine.ClassID,
                    InsuranceClass = Insureanceclass,
                    LineOfBusiness = insuranceLine.LineOfBusiness,
                    Abbreviation = insuranceLine.Abbreviation,
                    CreatedBy = "Israa",
                    CreatedDate = DateTime.Now,
                    UpdatedBy = "",
                    UpdatedDate =DateTime.Now,
                    IsDeleted = false,
                };
                var entity =await _unitOfWork.InsuranceLOB.AddEntityAsync(insuranceLine1);
                if (entity != null)
                {
                    await _unitOfWork.CommitAsync();
                    return true;
                }

            }
            return false;
        }

        public async Task<bool> DeleteLOBAsync(int id)
        {
            var insuranceLine = _unitOfWork.InsuranceLOB.GetEntityByIdAsync(id);

            if (insuranceLine != null)
            {
                 var success =await _unitOfWork.InsuranceLOB.DeleteEntityAsync(id);
                await _unitOfWork.CommitAsync();
                if (success)
                {
                    return true;
                }
                return false;
            }
            return false;
        }

        public async Task<List<GetInsuranceLOBDTO>> GetAllLOBsAsync()
        {
            var masterClasses = await _unitOfWork.GInsuranceClass.GetAllEntitytiesAsync();
            var insuranceLinees = await _unitOfWork.InsuranceLOB.GetAllEntitytiesAsync().ContinueWith(t =>
            {
                if (t.Result != null)
                {
                    List<GetInsuranceLOBDTO> insuranceLinees = t.Result.Select(i => new GetInsuranceLOBDTO
                    {
                        Id = i.Id,
                        LineOfBusiness = i.LineOfBusiness,
                        Abbreviation = i.Abbreviation,
                        ClassID = i.ClassID,
                        ClassName = masterClasses.FirstOrDefault(c=>c.Id == i.ClassID)?.ClassName ?? "N/A", // Note: This is a synchronous call inside a loop, consider optimizing this
                        CreatedBy = i.CreatedBy,
                        CreatedDate = i.CreatedDate,
                        UpdatedBy = i.UpdatedBy,
                        UpdatedDate = i.UpdatedDate,

                    }).ToList();

                    return insuranceLinees;
                }
                return null;
            });
            return insuranceLinees;
        }

        public async Task<List<GetInsuranceLOBDTO>> GetInsuranceLOBByClassIdAsync(int ClassId)
        {
            var LOB_List = await _unitOfWork.InsuranceLOB.GetInsuranceLOBByClassIdAsync(ClassId);
            List<GetInsuranceLOBDTO> dto = _mapper.Map<List<GetInsuranceLOBDTO>>(LOB_List);
            return dto;
        }

        public async Task<GetInsuranceLOBDTO> GetLOBByidAsync(int id)
        {
            var entity = await _unitOfWork.InsuranceLOB.GetEntityByIdAsync(id);

            if (entity == null) return null;

            // 3. Map the data to the DTO
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

            return dto;
        }

        public async Task<GetInsuranceLOBDTO> UpdateLOBAsync(UpdateInsuranceLOBDTO dto)
        {
            var Insureanceclass = await _unitOfWork.GInsuranceClass.GetEntityByIdAsync(dto.ClassID);

            // 1. Fetch the EXISTING record from the repository
            var entity = await _unitOfWork.InsuranceLOB.GetEntityByIdAsync(dto.Id);

            if (entity == null) return null;

            // 2. UPDATE the existing entity properties with data from the DTO
            // Do NOT create a 'new' entity; modify the one we just fetched
            entity.LineOfBusiness = dto.LineOfBusiness;
            entity.Abbreviation = dto.Abbreviation;
            entity.ClassID = dto.ClassID;
            entity.InsuranceClass = Insureanceclass;

            // Audit fields
            entity.UpdatedBy = "Israa";
            entity.UpdatedDate = DateTime.Now;

            // 3. Save the changes via the repository
            bool isUpdated = await _unitOfWork.InsuranceLOB.UpdateEntityAsync(entity);
            

            if (isUpdated)
            {
                await _unitOfWork.CommitAsync();
                // 4. Map the updated entity back to a GetinsuranceLineDTO for the UI
                return new GetInsuranceLOBDTO
                {
                    Id = entity.Id,
                    LineOfBusiness = entity.LineOfBusiness,
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
