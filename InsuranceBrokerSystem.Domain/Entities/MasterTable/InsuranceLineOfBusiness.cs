
namespace InsuranceBrokerSystem.Domain.Entities.MasterTable
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
