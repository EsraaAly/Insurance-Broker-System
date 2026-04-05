
namespace InsuranceBrokerSystem.Infrastructure.Repositories.Master_Table
{
    public class InsuranceLOBRepository: GenericRepository<InsuranceLineOfBusiness>,IInsuranceLOBRepository
    {
        private readonly AppDbContext _appDbContext;
        
        public InsuranceLOBRepository(AppDbContext appDbContext):base(appDbContext) 
        {
            _appDbContext = appDbContext;
        }

        public async Task<List<InsuranceLineOfBusiness>> GetInsuranceLOBByClassIdAsync(int ClassId)
        {
            return await _appDbContext.InsuranceLines.Where(i => i.ClassID == ClassId && i.IsDeleted == false).ToListAsync();
        }
    }
}
