
namespace InsuranceBrokerSystem.Domain.Entities.Master_Table
{
    public class InsuranceClass:BaseEntity
    {
        public string ClassName { get; set; }
        public string Abbreviation { get; set; }

        // Navigation
        public List<InsuranceLineOfBusiness> Lines { get; set; }
    }
}
