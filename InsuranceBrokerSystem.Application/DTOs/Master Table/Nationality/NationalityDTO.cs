namespace InsuranceBrokerSystem.Application.DTOs.Master_Table.Nationality
{
    public abstract class NationalityBase
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
    }

    public class AddNationalityDTO : NationalityBase
    {
    }

    public class GetNationalityDTO : NationalityBase
    {
        public int Id { get; set; }
        public string CreatedBy { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool IsDeleted { get; set; }
    }

    public class UpdateNationalityDTO : NationalityBase
    {
        public int Id { get; set; }
    }
}
