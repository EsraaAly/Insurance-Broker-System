
namespace InsuranceBrokerSystem.Application.Common.Mapping.MasterTable
{
    public class InsuranceMappingProfile:Profile
    {
        public InsuranceMappingProfile()
        {
            //CreateMap<GetInsuranceCompanyDTO, InsuranceCompany>();
            CreateMap<InsuranceCompany, GetInsuranceCompanyDTO>().ReverseMap();
            CreateMap<InsuranceCompany, AddInsuranceCompanyDTO>().ReverseMap();
            CreateMap<InsuranceCompany, UpdateInsuranceCompanyDTO>().ReverseMap();

            CreateMap<InsuranceContact, AddInsuranceContractDTO>().ReverseMap();
            CreateMap<InsuranceProduct, AddInsuranceProductDTO>().ReverseMap();

            CreateMap<InsuranceContact, GetInsuranceContractDTO>().ReverseMap();
            CreateMap<InsuranceProduct, GetInsuranceProductDTO>().ReverseMap();

            CreateMap<InsuranceContact, UpdateInsuranceContractDTO>().ReverseMap();
            CreateMap<InsuranceProduct, UpdateInsuranceProductDTO>().ReverseMap();

        }
    }
}
