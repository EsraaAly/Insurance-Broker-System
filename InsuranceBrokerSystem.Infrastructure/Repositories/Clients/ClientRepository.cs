namespace InsuranceBrokerSystem.Infrastructure.Repositories.Clients
{
    public class ClientRepository : GenericRepository<Client>, IClientRepository
    {
        private readonly AppDbContext _context;

        public ClientRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Client> GetClientByNameAsync(string clientName)
        {
            return await _context.Clients.FirstOrDefaultAsync(c => c.ClientName == clientName && !c.IsDeleted);
        }
        public async Task<bool> RejectClientAsync(int id)
        {

            var NoOfRows =  _context.Clients.Where(i => i.Id == id)
                                            .ExecuteUpdateAsync(c => c.SetProperty(p => p.IsRejected,true)  
                                                                      .SetProperty(p=>p.RejectedBy,"Israa")
                                                                      .SetProperty(p => p.RejectedDate, DateTime.Now));
            if (NoOfRows != null)
            {
                return true;
            }
            return false;
        }
        public async Task<bool> BlockClientAsync(int id)
        {
            var NoOfRows = _context.Clients.Where(i => i.Id == id)
                                                        .ExecuteUpdateAsync(c => c.SetProperty(p => p.IsBlocked, true)
                                                                                  .SetProperty(p => p.BlockedBy, "Israa")
                                                                                  .SetProperty(p => p.BlockedDate, DateTime.Now));
            if (NoOfRows != null)
            {
                return true;
            }
            return false;
        }
    }
}
