namespace InsuranceBrokerSystem.Application.DTOs.Master_Table.InsuranceContact
{
    public abstract class InsuranceContactBase
    {
        public string ContactName { get; set; }
        public string ContactEmail { get; set; }
        public string ContactMobileNo { get; set; }
        public string Department { get; set; }
        public int InsuranceCompanyId { get; set; }
    }

    public class AddInsuranceContactDTO : InsuranceContactBase
    {
    }

    public class GetInsuranceContactDTO : InsuranceContactBase
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

    public class UpdateInsuranceContactDTO : InsuranceContactBase
    {
        public int Id { get; set; }
        public bool IsRejected { get; set; }
        public bool IsApproved { get; set; }
    }
}
