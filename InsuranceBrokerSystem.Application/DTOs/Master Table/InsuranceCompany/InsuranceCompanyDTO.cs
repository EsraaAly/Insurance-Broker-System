
namespace InsuranceBrokerSystem.Application.DTOs.Master_Table.InsuranceCompany
{
    
     public abstract class InsuranceCompanyBase
     {
        public string CompanyName { get; set; }
        public string CompanyNameAr { get; set; }
        public string VATNo { get; set; }
        public string Tele { get; set; }
        public string Abbreviation { get; set; }
        public string CRNo { get; set; }
        public string Email { get; set; }

        public string UnifiedNo { get; set; }

        // 🔹 Address Details
        public string BuildingNo { get; set; }
        public string AdditionalNo { get; set; }
        public string BuildingName { get; set; }
        public string BuildingNameArabic { get; set; }
        public string StreetName { get; set; }
        public string StreetNameArabic { get; set; }
        public string DistrictName { get; set; }
        public string DistrictNameArabic { get; set; }
        public string PostalZIPCode { get; set; }
        public string CityName { get; set; }
        public string CityNameArabic { get; set; }
        public string State { get; set; }
        public string StateArabic { get; set; }
        public string CountryRegion { get; set; }
        public string CountryRegionArabic { get; set; }
    }

    public class AddInsuranceCompanyDTO: InsuranceCompanyBase
    {
        
        public string? AccNoCommAccrued { get; set; }
        public string? AccNoCommDue { get; set; }
        public string? AccNoVATAccrued { get; set; }
        public string? AccNoVATReceivable { get; set; }
        public string? AccNoGrossPremium { get; set; }
        public string? AccNoGrossVAT { get; set; }
        public string? AccNoNetPremium { get; set; }
        public string? AccNoUWVATPayable { get; set; }

        public List<AddInsuranceProductDTO> Products { get; set; } = new List<AddInsuranceProductDTO>();
        public List<AddInsuranceContractDTO> Contacts { get; set; } = new List<AddInsuranceContractDTO>();
    }

    public class ApprovedInsuranceCompanyDTO 
    {
        public int Id { get; set; }
        public string? AccNoCommAccrued { get; set; }
        public string? AccNoCommDue { get; set; }
        public string? AccNoVATAccrued { get; set; }
        public string? AccNoVATReceivable { get; set; }
        public string? AccNoGrossPremium { get; set; }
        public string? AccNoGrossVAT { get; set; }
        public string? AccNoNetPremium { get; set; }
        public string? AccNoUWVATPayable { get; set; }

        public bool IsApproved { get; set; }

        public string? ApprovedBy { get; set; }
        public DateTime? ApprovedDate { get; set; }
    }

    public class GetInsuranceCompanyDTO : InsuranceCompanyBase
    {
        public int Id { get; set; }
        
        public string? AccNoCommAccrued { get; set; }
        public string? AccNoCommDue { get; set; }
        public string? AccNoVATAccrued { get; set; }
        public string? AccNoVATReceivable { get; set; }
        public string? AccNoGrossPremium { get; set; }
        public string? AccNoGrossVAT { get; set; }
        public string? AccNoNetPremium { get; set; }
        public string? AccNoUWVATPayable { get; set; }

        public bool IsApproved { get; init; }
        public bool IsRejected { get; init; }

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
        public string? ApprovedBy { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public string? RejectedBy { get; set; }
        public DateTime? RejectedDate { get; set; }

        public List<GetInsuranceProductDTO> Products { get; set; } = new List<GetInsuranceProductDTO>();
        public List<GetInsuranceContractDTO> Contacts { get; set; } = new List<GetInsuranceContractDTO>();
    }

    public class UpdateInsuranceCompanyDTO : InsuranceCompanyBase
    {
        public int Id { get; set; }

        public List<UpdateInsuranceProductDTO> Products { get; set; } = new List<UpdateInsuranceProductDTO>();
        public List<UpdateInsuranceContractDTO> Contacts { get; set; } = new List<UpdateInsuranceContractDTO>();
    }

}
