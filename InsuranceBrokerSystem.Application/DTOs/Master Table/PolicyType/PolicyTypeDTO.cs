namespace InsuranceBrokerSystem.Application.DTOs.Master_Table.PolicyType
{
    public abstract class PolicyTypeBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class AddPolicyTypeDTO : PolicyTypeBase
    {
    }

    public class GetPolicyTypeDTO : PolicyTypeBase
    {
        public int Id { get; set; }
        public string CreatedBy { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool IsDeleted { get; set; }
        public override string ToString()
        {
            return Name; 
        }
    }

    public class UpdatePolicyTypeDTO : PolicyTypeBase
    {
        public int Id { get; set; }
        
    }
}
