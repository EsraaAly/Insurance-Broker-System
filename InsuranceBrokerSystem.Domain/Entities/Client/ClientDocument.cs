namespace InsuranceBrokerSystem.Domain.Entities.Client
{
    /// <summary>
    /// Represents a document (file) attached to a Client record.
    /// </summary>
    public class ClientDocument : BaseEntity
    {
        public int    ClientId     { get; set; }
        public string FileName     { get; set; } = string.Empty;
        public string FilePath     { get; set; } = string.Empty;
        public string DocumentType { get; set; } = string.Empty;
        public string FileSize     { get; set; } = string.Empty;
        public DateTime UploadDate { get; set; } = DateTime.UtcNow;

        // Navigation
        public Client? Client      { get; set; }
    }
}
