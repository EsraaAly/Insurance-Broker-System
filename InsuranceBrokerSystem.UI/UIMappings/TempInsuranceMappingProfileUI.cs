using InsuranceBrokerSystem.Domain.Entities.Master_Table;

namespace InsuranceBrokerSystem.UI.UI.MasterTable
{
    public class InsuranceMappingProfileUI:Profile
    {
        public InsuranceMappingProfileUI()
        {
            //CreateMap<GetInsuranceCompanyDTO, InsuranceCompany>();
            CreateMap<Contact, AddInsuranceContractDTO>();
            CreateMap<LinkedProduct, AddInsuranceProductDTO>();

        }
    }
}
