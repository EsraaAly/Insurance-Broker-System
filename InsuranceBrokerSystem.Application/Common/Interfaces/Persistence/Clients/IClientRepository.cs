
namespace InsuranceBrokerSystem.Application.Common.Interfaces.Persistence.Clients
{
    public interface IClientRepository : IGenericRepository<Client>
    {
        Task<Client> GetClientByNameAsync(string clientName);
        public Task<bool> BlockClientAsync(int id);
        public Task<bool> RejectClientAsync(int id);
    }
}
