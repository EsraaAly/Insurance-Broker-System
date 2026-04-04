
namespace InsuranceBrokerSystem.Application.Common.Mapping.MasterTable
{
    public class InsuranceLOBMappingProfile:Profile
    {
        public InsuranceLOBMappingProfile()
        { 
            CreateMap<InsuranceLineOfBusiness,GetInsuranceLOBDTO>().ReverseMap();
        }
    }
}
