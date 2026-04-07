/*
namespace InsuranceBrokerSystem.UI.UIMappings
{
    public class AccountMappingProfileUI:Profile
    {
        public AccountMappingProfileUI() 
        {
            //CreateMap<GetInsuranceCompanyDTO, InsuranceCompany>();
            CreateMap<Account, CreateAccountDTO>().ReverseMap();
            CreateMap<Account, EditAccountDTO>().ReverseMap();
            CreateMap<GetAccountDTO, Account>()
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.AccountType));
        }
    }
}
*/
