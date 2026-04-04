
namespace InsuranceBrokerSystem.Application.DTOs.Financial.InsuranceCompanyAccount
{
    public class GenerateInsuranceAccountsDTO
    {
        public int InsuranceCompanyId { get; set; }

        // Grouping the parent IDs for the 8 required accounts
        public int AccNoCommAccruedParentId { get; set; }
        public int AccNoCommDueParentId { get; set; }
        public int AccNoVATAccruedParentId { get; set; }
        public int AccNoVATReceivableParentId { get; set; }
        public int AccNoGrossPremiumParentId { get; set; }
        public int AccNoGrossVATParentId { get; set; }
        public int AccNoNetPremiumParentId { get; set; }
        public int AccNoUWVATPayableParentId { get; set; }
    }
}
