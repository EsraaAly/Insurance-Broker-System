namespace InsuranceBrokerSystem.Domain.Entities.Client
{
    /// <summary>
    /// Represents a bank account linked to a Client.
    /// </summary>
    public class ClientBankAccount : BaseEntity
    {
        public int    ClientId  { get; set; }
        public string BankName  { get; set; } = string.Empty;
        public string Branch    { get; set; } = string.Empty;
        public string IBAN      { get; set; } = string.Empty;
        public string SwiftCode { get; set; } = string.Empty;

        // Navigation
        public Client? Client   { get; set; }
    }
}
