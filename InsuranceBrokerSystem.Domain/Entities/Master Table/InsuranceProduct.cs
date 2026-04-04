
namespace InsuranceBrokerSystem.Domain.Entities.Master_Table
{
    public class InsuranceProduct: BaseEntity
    {
        public int ClassId { get; set; }
        public int LineOfBusinessId { get; set; }
        public bool IsRejected { get; set; }
        public bool IsApproved { get; set; }
        public int InsuranceCompanyId { get; set; }
        public InsuranceCompany InsuranceCompany { get; set; }
    }
}
