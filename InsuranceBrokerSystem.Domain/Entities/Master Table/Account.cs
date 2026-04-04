namespace InsuranceBrokerSystem.Domain.Entities.Master_Table
{
    public class Account:BaseEntity
    {
        public string AccountNumber { get; set; }
        public string AccountName { get; set; }
        public int? ParentId { get; set; }
        public virtual Account Parent { get; set; }
        public int Level { get; set; }
        public AccountType AccountType { get; set; }
        public string Description { get; set; }
        public bool IsPostable { get; set; }
        public virtual ICollection<Account> Children { get; set; } = new List<Account>();

    }
}
