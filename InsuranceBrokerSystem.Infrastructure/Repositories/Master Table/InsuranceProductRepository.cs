
namespace InsuranceBrokerSystem.Infrastructure.Repositories.Master_Table
{
    public class InsuranceProductRepository : GenericRepository<InsuranceProduct>,IInsuranceProductRepository
    {
        private readonly AppDbContext _context;

        public InsuranceProductRepository(AppDbContext context):base(context)
        {
            _context = context;
        }

        public async Task<List<InsuranceProduct>> GetInsuranceProductsByInsuranceIdAsync(int id)
        {
            return await  _context.InsuranceProducts.Where(p=>p.IsDeleted ==false && p.InsuranceCompanyId == id).ToListAsync();
        }
    }
}
