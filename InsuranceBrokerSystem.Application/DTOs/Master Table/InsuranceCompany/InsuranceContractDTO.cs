
namespace InsuranceBrokerSystem.Application.DTOs.Master_Table.InsuranceCompany
{
    public abstract class InsuranceContractBase
    {
        public string ContactName { get; set; }
        public string ContactEmail { get; set; }
        public string ContactMobileNo { get; set; }
        public string Department { get; set; }

        public int InsuranceCompanyId { get; set; }
    }
    public class AddInsuranceContractDTO: InsuranceContractBase
    {
    }
    public class GetInsuranceContractDTO: InsuranceContractBase
    {
        public int Id { get; set; }

        public string CreatedBy { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }

    }
    public class UpdateInsuranceContractDTO: InsuranceContractBase
    {
        public int Id { get; set; }
        public string ContactName { get; set; }
        public string ContactEmail { get; set; }
        public string ContactMobileNo { get; set; }
        public string Department { get; set; }

        public int InsuranceCompanyId { get; set; }
    }
}
