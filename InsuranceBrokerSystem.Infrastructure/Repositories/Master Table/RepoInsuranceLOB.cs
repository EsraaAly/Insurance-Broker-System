
namespace InsuranceBrokerSystem.Infrastructure.Repositories.Master_Table
{
    public class RepoInsuranceLOB: GenericRepository<InsuranceLineOfBusiness>,IRepoInsuranceLOB
    {
        private readonly AppDbContext _appDbContext;
        
        public RepoInsuranceLOB(AppDbContext appDbContext):base(appDbContext) 
        {
            _appDbContext = appDbContext;
        }

        public async Task<List<InsuranceLineOfBusiness>> GetInsuranceLOBByClassIdAsync(int ClassId)
        {
            return await _appDbContext.InsuranceLines.Where(i => i.ClassID == ClassId && i.IsDeleted == false).ToListAsync();
        }
    }
}
