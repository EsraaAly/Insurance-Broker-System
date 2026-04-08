
namespace InsuranceBrokerSystem.Domain.Entities.MasterTable
{
    public class InsuranceContact:BaseEntity
    {
        public string ContactName { get; set; }
        public string ContactEmail { get; set; }
        public string ContactMobileNo { get; set; }
        public string Department { get; set; }
        public bool IsRejected { get; set; }
        public bool IsApproved { get; set; }

        public int InsuranceCompanyId { get; set; }
        public InsuranceCompany InsuranceCompany { get; set; }
    }
}
