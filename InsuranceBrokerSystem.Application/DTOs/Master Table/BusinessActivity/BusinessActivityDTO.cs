namespace InsuranceBrokerSystem.Application.DTOs.Master_Table.BusinessActivity
{
    public abstract class BusinessActivityBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class AddBusinessActivityDTO : BusinessActivityBase
    {
    }

    public class GetBusinessActivityDTO : BusinessActivityBase
    {
        public int Id { get; set; }
        public string CreatedBy { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool IsDeleted { get; set; }
    }

    public class UpdateBusinessActivityDTO : BusinessActivityBase
    {
        public int Id { get; set; }
    }
}
