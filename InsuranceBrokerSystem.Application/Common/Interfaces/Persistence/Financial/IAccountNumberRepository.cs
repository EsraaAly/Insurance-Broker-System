
namespace InsuranceBrokerSystem.Application.Common.Interfaces.Persistence.Financial
{
    public interface IAccountNumberRepository:IGenericRepository<Account>
    {
        //public Task<string> GenerateAsync(int? parentId);

        public Task<string> GenerateAsync(Account? parent, IEnumerable<Account>? siblings,int AccountType);
    
    }
}
