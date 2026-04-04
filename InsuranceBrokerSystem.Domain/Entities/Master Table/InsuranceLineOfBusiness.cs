
namespace InsuranceBrokerSystem.Domain.Entities.Master_Table
{
    public class InsuranceLineOfBusiness:BaseEntity
    {
        public int ClassID { get; set; } // FK (string)

        public string LineOfBusiness { get; set; }
        public string Abbreviation { get; set; }

        // Navigation
        public InsuranceClass InsuranceClass { get; set; }
    }
}
