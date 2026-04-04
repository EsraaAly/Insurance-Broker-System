
namespace InsuranceBrokerSystem.Infrastructure.Repositories.Master_Table
{
    public class RepoInsuranceContact : GenericRepository<InsuranceContact>,IRepoInsuranceContact
    {
        private readonly AppDbContext _context;

        public RepoInsuranceContact(AppDbContext context):base(context) 
        {
            _context = context;
        }

        public async Task<List<InsuranceContact>> GetInsuranceContactsByInsuranceIdAsync(int id)
        {
             return  await _context.InsuranceContacts.Where(p => p.IsDeleted == false && p.InsuranceCompanyId == id).ToListAsync();
        }
    }
}
