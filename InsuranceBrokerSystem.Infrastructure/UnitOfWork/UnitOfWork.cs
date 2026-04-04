
namespace InsuranceBrokerSystem.Infrastructure.UnitOfWork
{
    public class UnitOfWork:IUnitOfWork
    {
        private readonly AppDbContext _context;

        private IGenericRepository<InsuranceClass> _GInsuranceClass;

        private IRepoInsuranceLOB _InsuranceLOB;
        private IRepoInsuranceCompany _InsuranceCompany;
        private IRepoInsuranceContact _InsuranceContract;
        private IRepoInsuranceProduct _InsuranceProduct;
        private IRepoAccountNumber _AccountNumber;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }
        public IGenericRepository<InsuranceClass> GInsuranceClass => _GInsuranceClass??= new GenericRepository<InsuranceClass>(_context);
        public IRepoInsuranceCompany InsuranceCompany => _InsuranceCompany??= new RepoInsuranceCompany(_context);        
        public IRepoInsuranceLOB InsuranceLOB => _InsuranceLOB ??= new RepoInsuranceLOB(_context);        
        public IRepoInsuranceContact InsuranceContract => _InsuranceContract ??= new RepoInsuranceContact(_context);
        public IRepoInsuranceProduct InsuranceProduct => _InsuranceProduct ??= new RepoInsuranceProduct(_context);        
        public IRepoAccountNumber AccountNumber => _AccountNumber ??= new RepoAccountNumber(_context);
        public async Task CommitAsync() => await _context.SaveChangesAsync();


        //public async void Dispose() => await _context.DisposeAsync();
    }
}
