
namespace InsuranceBrokerSystem.Infrastructure.Repositories.Master_Table
{
    public class RepoInsuranceProduct : GenericRepository<InsuranceProduct>,IRepoInsuranceProduct
    {
        private readonly AppDbContext _context;

        public RepoInsuranceProduct(AppDbContext context):base(context)
        {
            _context = context;
        }

        public async Task<List<InsuranceProduct>> GetInsuranceProductsByInsuranceIdAsync(int id)
        {
            return await  _context.InsuranceProducts.Where(p=>p.IsDeleted ==false && p.InsuranceCompanyId == id).ToListAsync();
        }
    }
}
