
using InsuranceBrokerSystem.Application.DTOs.Master_Table.Bank;

namespace InsuranceBrokerSystem.Application.DTOs.Client
{
    public abstract class ClientBankAccountBase
    {
        public int    ClientId  { get; set; }
        public int?   BankId   { get; set; }
        public string Branch    { get; set; } = string.Empty;
        public string IBAN      { get; set; } = string.Empty;
        public string SwiftCode { get; set; } = string.Empty;
    }

    public class AddClientBankAccountDTO : ClientBankAccountBase
    {
    }

    public class GetClientBankAccountDTO : ClientBankAccountBase
    {
        public int Id { get; set; }

        public string CreatedBy     { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }
        public string? UpdatedBy   { get; set; }
        public DateTime? UpdatedDate { get; set; }
        
        // Navigation property for Bank information
        public GetBankDTO? Bank { get; set; }
    }

    public class UpdateClientBankAccountDTO : ClientBankAccountBase
    {
        public int Id { get; set; }
    }
}
