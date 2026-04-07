using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InsuranceBrokerSystem.Domain.Entities.Client;
using InsuranceBrokerSystem.Domain.Entities.Financial;
using InsuranceBrokerSystem.Domain.Entities.Master_Table;
using InsuranceBrokerSystem.Application.DTOs.Client;
using InsuranceBrokerSystem.Application.DTOs.Financial.Account;
using InsuranceBrokerSystem.Application.DTOs.Master_Table.InsuranceCompany;
using InsuranceBrokerSystem.Application.DTOs.Master_Table.Insurance_Class_and_Line;

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
