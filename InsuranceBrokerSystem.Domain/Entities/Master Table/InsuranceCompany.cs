using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceBrokerSystem.Domain.Entities.Master_Table
{
    public class InsuranceCompany: BaseEntity
    {
        public string CompanyName { get; set; }
        public string CompanyNameAr { get; set; }

        public string VATNo { get; set; }
        public string Tele { get; set; }
        public string Abbreviation { get; set; }
        public string CRNo { get; set; }
        public string Email { get; set; }

        // 🔹 Accounting Fields
        public string? AccNoCommAccrued { get; set; }
        public string? AccNoCommDue { get; set; }
        public string? AccNoVATAccrued { get; set; }
        public string? AccNoVATReceivable { get; set; }
        public string? AccNoGrossPremium { get; set; }
        public string? AccNoGrossVAT { get; set; }
        public string? AccNoNetPremium { get; set; }
        public string? AccNoUWVATPayable { get; set; }

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

        public bool IsRejected { get; set; }
        public bool IsApproved { get; set; }

        public string? ApprovedBy { get; set; }
        public DateTime? ApprovedDate { get; set; }

        public string? RejectedBy { get; set; }
        public DateTime? RejectedDate { get; set; }

        public List<InsuranceProduct> Products { get; set; } = new List<InsuranceProduct>();
        public List<InsuranceContact> Contacts { get; set; } = new List<InsuranceContact>();
    }
}
