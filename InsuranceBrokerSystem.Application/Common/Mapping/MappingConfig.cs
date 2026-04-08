

namespace InsuranceBrokerSystem.Application.Common.Mapping
{
    public class MappingConfig
    {
        public static void Configure()
        {
            // Global Settings
            TypeAdapterConfig.GlobalSettings.Default.MaxDepth(3);

            // Insurance Company & Related
            TypeAdapterConfig<InsuranceCompany, GetInsuranceCompanyDTO>.NewConfig().TwoWays();
            TypeAdapterConfig<InsuranceCompany, AddInsuranceCompanyDTO>.NewConfig().TwoWays();
            TypeAdapterConfig<InsuranceCompany, UpdateInsuranceCompanyDTO>.NewConfig().TwoWays();
            TypeAdapterConfig<InsuranceContact, AddInsuranceContractDTO>.NewConfig().TwoWays();
            TypeAdapterConfig<InsuranceProduct, AddInsuranceProductDTO>.NewConfig().TwoWays();
            TypeAdapterConfig<InsuranceContact, GetInsuranceContractDTO>.NewConfig().TwoWays();
            TypeAdapterConfig<InsuranceProduct, GetInsuranceProductDTO>.NewConfig().TwoWays();
            TypeAdapterConfig<InsuranceContact, UpdateInsuranceContractDTO>.NewConfig().TwoWays();
            TypeAdapterConfig<InsuranceProduct, UpdateInsuranceProductDTO>.NewConfig().TwoWays();

            // Financial Accounts
            TypeAdapterConfig<Account, GetAccountDTO>.NewConfig().TwoWays();
            TypeAdapterConfig<CreateAccountDTO, Account>.NewConfig().TwoWays();
            TypeAdapterConfig<EditAccountDTO, Account>.NewConfig().TwoWays();

            // Insurance LOB
            TypeAdapterConfig<InsuranceLineOfBusiness, GetInsuranceLOBDTO>.NewConfig().TwoWays();

            // Clients
            TypeAdapterConfig<Client, GetClientDTO>.NewConfig().TwoWays();
            TypeAdapterConfig<Client, AddClientDTO>.NewConfig().TwoWays();
            TypeAdapterConfig<Client, UpdateClientDTO>.NewConfig().TwoWays();
            TypeAdapterConfig<ClientContact, AddClientContactDTO>.NewConfig().TwoWays();
            TypeAdapterConfig<ClientContact, GetClientContactDTO>.NewConfig().TwoWays();
            TypeAdapterConfig<ClientContact, UpdateClientContactDTO>.NewConfig().TwoWays();
            TypeAdapterConfig<ClientDocument, AddClientDocumentDTO>.NewConfig().TwoWays();
            TypeAdapterConfig<ClientDocument, GetClientDocumentDTO>.NewConfig().TwoWays();
            TypeAdapterConfig<ClientDocument, UpdateClientDocumentDTO>.NewConfig().TwoWays();
            TypeAdapterConfig<ClientBankAccount, AddClientBankAccountDTO>.NewConfig().TwoWays();
            TypeAdapterConfig<ClientBankAccount, GetClientBankAccountDTO>.NewConfig().TwoWays();
            TypeAdapterConfig<ClientBankAccount, UpdateClientBankAccountDTO>.NewConfig().TwoWays();

            // Compile settings to catch errors early
            TypeAdapterConfig.GlobalSettings.Scan(typeof(MappingConfig).Assembly);
        }
    }
}
