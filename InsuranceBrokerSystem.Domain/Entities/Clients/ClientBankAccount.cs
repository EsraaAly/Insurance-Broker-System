using InsuranceBrokerSystem.Domain.Entities.MasterTable;

namespace InsuranceBrokerSystem.Domain.Entities.Clients
{
    /// <summary>
    /// Represents a bank account linked to a Client.
    /// </summary>
    public class ClientBankAccount : BaseEntity
    {
        public int    ClientId  { get; set; }
        public int?   BankId   { get; set; }
        public string BankName  { get; set; } = string.Empty; // Keep for backward compatibility
        public string Branch    { get; set; } = string.Empty;
        public string IBAN      { get; set; } = string.Empty;
        public string SwiftCode { get; set; } = string.Empty;

        // Navigation
        public Client? Client   { get; set; }
        public Bank?   Bank     { get; set; }
    }
}
