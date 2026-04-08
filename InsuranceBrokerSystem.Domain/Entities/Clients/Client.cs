

namespace InsuranceBrokerSystem.Domain.Entities.Clients
{
    /// <summary>
    /// Represents a Brokerage Client (Retail or Corporate).
    /// Aggregates contact persons, documents, and bank accounts.
    /// </summary>
    public class Client : BaseEntity
    {
        // ─── Core Identity ────────────────────────────────────────────────
        public string ClientName          { get; set; } = string.Empty;
        public string ClientNameAr        { get; set; } = string.Empty;
        public string OfficialName        { get; set; } = string.Empty;

        // ─── Classification ───────────────────────────────────────────────
        public int?              PolicyTypeId         { get; set; }
        public ClientType        ClientType           { get; set; } = ClientType.Retail;
        public RelationshipStatus ClientRelationshipStatus  { get; set; } = RelationshipStatus.Captive;
        public ClientStatus      Status               { get; set; } = ClientStatus.Prospect;

        // ─── Retail Fields ────────────────────────────────────────────────
        public string?   IdentityNo        { get; set; }
        public DateTime? IDExpiryDate      { get; set; }
        public string?   IDExpiryDateHijri { get; set; }
        public int?      NationalityId     { get; set; }
        public int?      SourceOfIncomeId  { get; set; }
        public string?   Email             { get; set; }
        public DateTime? DateOfBirth       { get; set; }

        // ─── Corporate Fields ─────────────────────────────────────────────
        public CorporateRegistrationStatus? RegistrationStatus { get; set; }
        public int?      BusinessActivityId           { get; set; }
        public string?   MarketSegment               { get; set; }
        public DateTime? DateOfIncorporation         { get; set; }
        public string?   DateOfIncorporationHijri    { get; set; }
        public string?   CommercialRegistrationNo    { get; set; }
        public DateTime? CRExpiryDate                { get; set; }
        public string?   CRExpiryDateHijri           { get; set; }
        public string?   SponsorId                   { get; set; }
        public string?   UnifiedNo                   { get; set; }
        public string?   VATNo                       { get; set; }
        public decimal?  Capital                     { get; set; }
        public string?   PremiumClass                { get; set; }

        // ─── National Address ─────────────────────────────────────────────
        public int?      LocationId  { get; set; }
        public string?   BuildingNo  { get; set; }
        public string?   District    { get; set; }
        public string?   POBox       { get; set; }
        public string?   Tele        { get; set; }
        public string?   Fax         { get; set; }
        public string?   Channel     { get; set; }
        public string?   Interface   { get; set; }
        public string?   Producer { get; set; }
        public string?   Producer2 { get; set; }
        public string?   ScreeningResult { get; set; }
        public string?   IBANNumber  { get; set; }

        // ─── Accounts ─────────────────────────────────────────────
        public string? AccountPremium { get; set; }

        public bool IsRejected { get; set; }
        public bool IsApproved { get; set; }
        public bool IsBlocked { get; set; }

        public string? ApprovedBy { get; set; }
        public DateTime? ApprovedDate { get; set; }

        public string? RejectedBy { get; set; }
        public DateTime? RejectedDate { get; set; }

        public string? BlockedBy { get; set; }
        public DateTime? BlockedDate { get; set; }

        // ─── Navigation Properties ────────────────────────────────────────
        public List<ClientContact>     Contacts     { get; set; } = [];
        public List<ClientDocument>    Documents    { get; set; } = [];
        public List<ClientBankAccount> BankAccounts { get; set; } = [];
        
        // Master Data Navigation Properties
        public PolicyType?         PolicyType         { get; set; }
        public Nationality? ClientNationality { get; set; }
        public SourceOfIncome?    SourceOfIncome    { get; set; }
        public BusinessActivity?  BusinessActivity  { get; set; }
        public Location?           Location           { get; set; }
    }
}
