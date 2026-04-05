
namespace InsuranceBrokerSystem.Infrastructure.Repositories.Master_Table
{
    public class InsuranceContactRepository : GenericRepository<InsuranceContact>,IInsuranceContactRepository
    {
        private readonly AppDbContext _context;

        public InsuranceContactRepository(AppDbContext context):base(context) 
        {
            _context = context;
        }

        public async Task<List<InsuranceContact>> GetInsuranceContactsByInsuranceIdAsync(int id)
        {
             return  await _context.InsuranceContacts.Where(p => p.IsDeleted == false && p.InsuranceCompanyId == id).ToListAsync();
        }
    }
}
