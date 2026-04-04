
namespace InsuranceBrokerSystem.Application.DTOs.Master_Table.InsuranceCompany
{
    public class InsuranceProductBase
    {
        public int ClassId { get; set; }
        public int LineOfBusinessId { get; set; }

        public int InsuranceCompanyId { get; set; }
    }

    public class AddInsuranceProductDTO: InsuranceProductBase
    {

    }

    public class GetInsuranceProductDTO: InsuranceProductBase
    {
        public int Id { get; set; }
        public string CreatedBy { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }

    }

    public class UpdateInsuranceProductDTO: InsuranceProductBase
    {
        public int Id { get; set; }

    }
}
