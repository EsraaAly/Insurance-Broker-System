using Mapster;
using InsuranceBrokerSystem.UI.Views.MasterTable;
using InsuranceBrokerSystem.Domain.Entities.Financial;
using InsuranceBrokerSystem.Application.DTOs.Financial.Account;
using InsuranceBrokerSystem.Application.DTOs.Master_Table.InsuranceCompany;

namespace InsuranceBrokerSystem.UI.Common.Mapping
{
    public class UIMappingConfig
    {
        public static void Configure()
        {
            // Account UI Mappings
            //TypeAdapterConfig<Account, CreateAccountDTO>.NewConfig().TwoWays();
            //TypeAdapterConfig<Account, EditAccountDTO>.NewConfig().TwoWays();
            //TypeAdapterConfig<GetAccountDTO, Account>.NewConfig()
            //    .Map(dest => dest.Type, src => src.AccountType)
            //    .TwoWays();

            // Insurance Company UI Mappings
            TypeAdapterConfig<Contact, AddInsuranceContractDTO>.NewConfig();
            TypeAdapterConfig<LinkedProduct, AddInsuranceProductDTO>.NewConfig();
        }
    }
}
