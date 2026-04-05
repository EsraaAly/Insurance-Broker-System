
namespace InsuranceBrokerSystem.Application.DTOs.Client
{
    public abstract class ClientDocumentBase
    {
        public int    ClientId     { get; set; }
        public string FileName     { get; set; } = string.Empty;
        public string FilePath     { get; set; } = string.Empty;
        public string DocumentType { get; set; } = string.Empty;
        public string FileSize     { get; set; } = string.Empty;
    }

    public class AddClientDocumentDTO : ClientDocumentBase
    {
    }

    public class GetClientDocumentDTO : ClientDocumentBase
    {
        public int Id { get; set; }

        public DateTime UploadDate  { get; set; }

        public string CreatedBy     { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }
        public string? UpdatedBy   { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }

    public class UpdateClientDocumentDTO : ClientDocumentBase
    {
        public int Id { get; set; }
    }
}
