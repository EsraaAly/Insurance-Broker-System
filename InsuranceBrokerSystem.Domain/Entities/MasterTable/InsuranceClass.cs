
namespace InsuranceBrokerSystem.Domain.Entities.MasterTable
{
    public class InsuranceClass:BaseEntity
    {
        public string ClassName { get; set; }
        public string Abbreviation { get; set; }

        // Navigation
        public List<InsuranceLineOfBusiness> Lines { get; set; }
    }
}
