
namespace InsuranceBrokerSystem.Infrastructure.UnitOfWork
{
    public class UnitOfWork:IUnitOfWork
    {
        private readonly AppDbContext _context;

        private IGenericRepository<InsuranceClass> _GInsuranceClass;

        private IInsuranceLOBRepository _InsuranceLOBRepository;
        private IInsuranceCompanyRepository _InsuranceCompanyRepository;
        private IInsuranceContactRepository _InsuranceContactRepository;
        private IInsuranceProductRepository _InsuranceProductRepository;
        private IAccountNumberRepository _AccountNumberRepository;
        private IClientRepository _ClientRepository;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }
        public IGenericRepository<InsuranceClass> GInsuranceClass => _GInsuranceClass??= new GenericRepository<InsuranceClass>(_context);
        public IInsuranceCompanyRepository InsuranceCompanyRepository => _InsuranceCompanyRepository??= new InsuranceCompanyRepository(_context);        
        public IInsuranceLOBRepository InsuranceLOBRepository => _InsuranceLOBRepository ??= new InsuranceLOBRepository(_context);        
        public IInsuranceContactRepository InsuranceContractRepository => _InsuranceContactRepository ??= new InsuranceContactRepository(_context);
        public IInsuranceProductRepository InsuranceProductRepository => _InsuranceProductRepository ??= new InsuranceProductRepository(_context);        
        public IAccountNumberRepository AccountNumberRepository => _AccountNumberRepository ??= new AccountNumberRepository(_context);
        public IClientRepository ClientRepository => _ClientRepository ??= new ClientRepository(_context);
        public async Task CommitAsync() => await _context.SaveChangesAsync();


        //public async void Dispose() => await _context.DisposeAsync();
    }
}
