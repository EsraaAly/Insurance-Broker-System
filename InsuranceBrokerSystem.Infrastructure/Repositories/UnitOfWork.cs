namespace InsuranceBrokerSystem.Infrastructure.Repositories
{
    public class UnitOfWork:IUnitOfWork
    {
        private readonly AppDbContext _context;

        private IGenericRepository<InsuranceClass> _GInsuranceClass;
        private IGenericRepository<PolicyType> _GPolicyType;
        private IGenericRepository<BusinessActivity> _GBusinessActivity;
        private IGenericRepository<Nationality> _GNationality;
        private IGenericRepository<Location> _GLocation;
        private IGenericRepository<SourceOfIncome> _GSourceOfIncome;

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
        public IGenericRepository<PolicyType> GPolicyType => _GPolicyType ??= new GenericRepository<PolicyType>(_context);
        public IGenericRepository<BusinessActivity> GBusinessActivity => _GBusinessActivity ??= new GenericRepository<BusinessActivity>(_context);
        public IGenericRepository<Nationality> GNationality => _GNationality ??= new GenericRepository<Nationality>(_context);
        public IGenericRepository<Location> GLocation => _GLocation ??= new GenericRepository<Location>(_context);
        public IGenericRepository<SourceOfIncome> GSourceOfIncome => _GSourceOfIncome ??= new GenericRepository<SourceOfIncome>(_context);
        public IInsuranceCompanyRepository InsuranceCompanyRepository => _InsuranceCompanyRepository??= new InsuranceCompanyRepository(_context);        
        public IInsuranceContactRepository InsuranceContractRepository => _InsuranceContactRepository ??= new InsuranceContactRepository(_context);
        public IInsuranceProductRepository InsuranceProductRepository => _InsuranceProductRepository ??= new InsuranceProductRepository(_context);        
        public IAccountNumberRepository AccountNumberRepository => _AccountNumberRepository ??= new AccountNumberRepository(_context);
        public IClientRepository ClientRepository => _ClientRepository ??= new ClientRepository(_context);

        public IInsuranceLOBRepository InsuranceLOBRepository => _InsuranceLOBRepository ??= new InsuranceLOBRepository(_context);

        public async Task CommitAsync() => await _context.SaveChangesAsync();


        //public async void Dispose() => await _context.DisposeAsync();
    }
}
