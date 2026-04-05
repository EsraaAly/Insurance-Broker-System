
namespace InsuranceBrokerSystem.Application.DTOs.Client
{
    /// <summary>
    /// Shared fields across all Client DTO variants.
    /// </summary>
    public abstract class ClientBase
    {
        // ─── Core Identity ────────────────────────────────────────────────
        public string ClientName          { get; set; } = string.Empty;
        public string ClientNameAr        { get; set; } = string.Empty;
        public string OfficialName        { get; set; } = string.Empty;

        // ─── Classification ───────────────────────────────────────────────
        public int?   PolicyTypeId        { get; set; }
        public int    ClientType          { get; set; }
        public int    RelationshipStatus  { get; set; }
        //public int    Status              { get; set; }

        public bool IsRejected { get; init; }
        public bool IsApproved { get; init; }
        public bool IsBlocked { get; init; }

        public int Status
        {
            get
            {
                if (IsBlocked) return 3;
                if (IsRejected) return 4;
                if (IsApproved) return 2;
                return 1;
            }
        }

        // ─── Retail Fields ────────────────────────────────────────────────
        public string?   IdentityNo        { get; set; }
        public DateTime? IDExpiryDate      { get; set; }
        public string?   IDExpiryDateHijri { get; set; }
        public string?   Nationality       { get; set; }
        public string?   SourceOfIncome    { get; set; }
        public string?   Email             { get; set; }
        public DateTime? DateOfBirth       { get; set; }

        // ─── Corporate Fields ─────────────────────────────────────────────
        public string?   RegistrationStatus       { get; set; }
        public string?   BusinessActivity         { get; set; }
        public string?   MarketSegment            { get; set; }
        public DateTime? DateOfIncorporation      { get; set; }
        public string?   DateOfIncorporationHijri { get; set; }
        public string?   CommercialRegistrationNo { get; set; }
        public DateTime? CRExpiryDate             { get; set; }
        public string?   CRExpiryDateHijri        { get; set; }
        public string?   SponsorId                { get; set; }
        public string?   UnifiedNo                { get; set; }
        public string?   VATNo                    { get; set; }
        public decimal?  Capital                  { get; set; }
        public string?   PremiumClass             { get; set; }

        // ─── National Address ─────────────────────────────────────────────
        public string?   Location        { get; set; }
        public string?   BuildingNo      { get; set; }
        public string?   District        { get; set; }
        public string?   POBox           { get; set; }
        public string?   Tele            { get; set; }
        public string?   Fax             { get; set; }
        public string?   Channel         { get; set; }
        public string?   Interface       { get; set; }
        public string?   ProducerId      { get; set; }
        public string?   Producer2Id     { get; set; }
        public string?   ScreeningResult { get; set; }
        public string?   IBANNumber      { get; set; }
    }

    public class AddClientDTO : ClientBase
    {
        public List<AddClientContactDTO>     Contacts     { get; set; } = new();
        public List<AddClientDocumentDTO>    Documents    { get; set; } = new();
        public List<AddClientBankAccountDTO> BankAccounts { get; set; } = new();
    }
    public class GetClientDTO : ClientBase
    {
        public int Id { get; set; }

        public string? AccountPremium { get; set; }

        public string? ApprovedBy { get; set; }
        public DateTime? ApprovedDate { get; set; }

        public string? RejectedBy { get; set; }
        public DateTime? RejectedDate { get; set; }

        public string? BlockedBy { get; set; }
        public DateTime? BlockedDate { get; set; }

        public List<GetClientContactDTO>     Contacts     { get; set; } = new();
        public List<GetClientDocumentDTO>    Documents    { get; set; } = new();
        public List<GetClientBankAccountDTO> BankAccounts { get; set; } = new();
    }
    public class ApproveClientDTO 
    {
        public int Id { get; set; }

        public string? AccountPremium { get; set; }

        public string? ApprovedBy { get; set; }
        public DateTime? ApprovedDate { get; set; }
    }
    public class UpdateClientDTO : ClientBase
    {
        public int Id { get; set; }

        public List<UpdateClientContactDTO>     Contacts     { get; set; } = new();
        public List<UpdateClientDocumentDTO>    Documents    { get; set; } = new();
        public List<UpdateClientBankAccountDTO> BankAccounts { get; set; } = new();
    }
}
