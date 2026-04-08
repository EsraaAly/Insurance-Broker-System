namespace InsuranceBrokerSystem.Application.DTOs.Master_Table.SourceOfIncome
{
    public abstract class SourceOfIncomeBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class AddSourceOfIncomeDTO : SourceOfIncomeBase
    {
    }

    public class GetSourceOfIncomeDTO : SourceOfIncomeBase
    {
        public int Id { get; set; }
        public string CreatedBy { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool IsDeleted { get; set; }
    }

    public class UpdateSourceOfIncomeDTO : SourceOfIncomeBase
    {
        public int Id { get; set; }
    }
}
