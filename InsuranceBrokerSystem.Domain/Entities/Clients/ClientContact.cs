namespace InsuranceBrokerSystem.Domain.Entities.Clients
{
    /// <summary>
    /// Represents a contact person associated with a Client.
    /// </summary>
    public class ClientContact : BaseEntity
    {
        public int    ClientId  { get; set; }
        public string Name      { get; set; } = string.Empty;
        public string Position  { get; set; } = string.Empty;
        public string Extension { get; set; } = string.Empty;
        public string Mobile    { get; set; } = string.Empty;
        public string Tele      { get; set; } = string.Empty;
        public string Email     { get; set; } = string.Empty;
        public bool   SaddadInvoice { get; set; }
        public string? Branch   { get; set; }

        // Navigation
        public Client? Client   { get; set; }
    }
}
