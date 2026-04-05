
namespace InsuranceBrokerSystem.Infrastructure.Repositories.Master_Table
{
    public class InsuranceCompanyRepository : GenericRepository<InsuranceCompany>,IInsuranceCompanyRepository
    {
        private readonly AppDbContext _context;

        public InsuranceCompanyRepository(AppDbContext context):base(context) 
        {
            _context = context;
        }
        public async Task<InsuranceCompany> AddInsuranceCompaniesAsync(InsuranceCompany Entity)
        {
            await _context.InsuranceCompanies.AddAsync(Entity);

            return Entity;
        }
        //public async Task<bool> DeleteInsuranceCompaniesAsync(int Id)
        //{
        //    var success = await _context.InsuranceCompanies.Where(c => c.Id == Id).ExecuteUpdateAsync(i=>i.SetProperty(t=>t.IsDeleted,true));
        //    if(success == 0)
        //    {
        //        return false;
        //    }
        //    return true;
        //}
        //public async Task<List<InsuranceCompany>> GetAllInsuranceCompaniesAsync()
        //{
        //    return await _context.InsuranceCompanies.Where(i=>i.IsDeleted == false).ToListAsync();
        //}
        //public async Task<InsuranceCompany> GetInsuranceCompaniesByIdAsync(int Id)
        //{
        //    return await _context.InsuranceCompanies.FirstOrDefaultAsync(i=>i.IsDeleted ==false && i.Id == Id);
        //}
        public async Task<InsuranceCompany> GetInsuranceCompaniesByNameAsync(string CompanyName)
        {
            return await _context.InsuranceCompanies.FirstOrDefaultAsync(i => i.IsDeleted == false && i.CompanyName == CompanyName);
        }

        //public async Task<bool> UpdateInsuranceCompaniesAsync(InsuranceCompany Entity)
        //{
        //    var entity = _context.InsuranceCompanies.Update(Entity);
        //    if (entity == null)
        //    {
        //        return false;
        //    }
        //    return true;
        //}
        //public async Task<bool> ApproveInsuranceCompaniesAsync(InsuranceCompany insuranceCompany)
        //{
        //    var entity = _context.InsuranceCompanies.Update(insuranceCompany);
        //    if (entity == null)
        //    {
        //        return false;
        //    }
        //    return true;
        //}

        public async Task<bool> RejectInsuranceCompaniesAsync(int id)
        {
            var entity = await _context.InsuranceCompanies.Where(i => i.IsDeleted == false && i.Id == id && i.IsApproved == false).ExecuteUpdateAsync(c => c.SetProperty(p => p.IsRejected, true));
            if (entity == null)
            {
                return false;
            }
            return true;
        }
    }
}
