namespace InsuranceBrokerSystem.Application.DTOs.Master_Table.Bank
{
    public abstract class BankBase
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string SwiftCode { get; set; }
        public bool IsActive { get; set; } = true;
    }

    public class AddBankDTO : BankBase
    {
    }

    public class GetBankDTO : BankBase
    {
        public int Id { get; set; }
        public string CreatedBy { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool IsDeleted { get; set; }
        public override string ToString()
        {
            return Name;
        }
    }

    public class UpdateBankDTO : BankBase
    {
        public int Id { get; set; }
    }
}
