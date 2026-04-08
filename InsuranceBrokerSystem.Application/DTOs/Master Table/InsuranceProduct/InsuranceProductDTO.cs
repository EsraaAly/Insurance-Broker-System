namespace InsuranceBrokerSystem.Application.DTOs.Master_Table.InsuranceProduct
{
    public abstract class InsuranceProductBase
    {
        public int ClassId { get; set; }
        public int LineOfBusinessId { get; set; }
        public int InsuranceCompanyId { get; set; }
    }

    public class AddInsuranceProductDTO : InsuranceProductBase
    {
    }

    public class GetInsuranceProductDTO : InsuranceProductBase
    {
        public int Id { get; set; }
        public bool IsRejected { get; set; }
        public bool IsApproved { get; set; }
        
        public string Status
        {
            get
            {
                if (IsRejected) return "Rejected";
                if (IsApproved) return "Approved";
                return "Pending";
            }
        }

        public string CreatedBy { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool IsDeleted { get; set; }
        public string? ApprovedBy { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public string? RejectedBy { get; set; }
        public DateTime? RejectedDate { get; set; }
    }

    public class UpdateInsuranceProductDTO : InsuranceProductBase
    {
        public int Id { get; set; }
        public bool IsRejected { get; set; }
        public bool IsApproved { get; set; }
    }
}
