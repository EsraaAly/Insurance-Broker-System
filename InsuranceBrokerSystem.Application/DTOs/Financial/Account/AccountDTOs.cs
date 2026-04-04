
namespace InsuranceBrokerSystem.Application.DTOs.Financial.Account
{
    public class CreateAccountDTO
    {
        public string AccountName { get; set; }
        public int? ParentId { get; set; }
        public int Level { get; set; }
        public AccountType AccountType { get; set; }
        public string Description { get; set; }
        public bool IsPostable { get; set; }
        public virtual ICollection<CreateAccountDTO> Children { get; set; } = new List<CreateAccountDTO>();

    }

    public class GetAccountDTO
    {
        public int Id { get; set; }
        public string AccountNumber { get; set; }
        public string AccountName { get; set; }
        public int? ParentId { get; set; }
        public int Level { get; set; }
        public AccountType AccountType { get; set; }
        public string Description { get; set; }
        public bool IsPostable { get; set; }
        public ICollection<GetAccountDTO> Children { get; set; } = new List<GetAccountDTO>();
    }
    public class EditAccountDTO
    {
        public int Id { get; set; }
        public string AccountName { get; set; }
        public string Description { get; set; }
        public bool IsPostable { get; set; }

    }
}
