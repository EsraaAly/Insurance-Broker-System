namespace InsuranceBrokerSystem.Application.DTOs.Master_Table.Location
{
    public abstract class LocationBase
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
    }

    public class AddLocationDTO : LocationBase
    {
    }

    public class GetLocationDTO : LocationBase
    {
        public int Id { get; set; }
        public string CreatedBy { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool IsDeleted { get; set; }
    }

    public class UpdateLocationDTO : LocationBase
    {
        public int Id { get; set; }
    }
}
