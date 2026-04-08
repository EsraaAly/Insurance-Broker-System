namespace InsuranceBrokerSystem.Application.Common.Interfaces.UnitOfWork
{
    public interface IUnitOfWork
    {

        IGenericRepository<InsuranceClass> GInsuranceClass { get; }
        IGenericRepository<PolicyType> GPolicyType { get; }
        IGenericRepository<BusinessActivity> GBusinessActivity { get; }
        IGenericRepository<Nationality> GNationality { get; }
        IGenericRepository<Location> GLocation { get; }
        IGenericRepository<SourceOfIncome> GSourceOfIncome { get; }
        //IGenericRepository<InsuranceCompany> G_repoInsuranceCompany { get; }
        //IGenericRepository<InsuranceContact> G_repoInsuranceContract { get; }
        //IGenericRepository<InsuranceProduct> G_repoInsuranceProduct { get; }
        //IGenericRepository<Account> G_repoAccount { get; }

        //IRepoInsuranceClass repoInsuranceClass { get; }

        IInsuranceLOBRepository InsuranceLOBRepository { get; }

        IInsuranceCompanyRepository InsuranceCompanyRepository { get; }
        IInsuranceContactRepository InsuranceContractRepository { get; }
        IInsuranceProductRepository InsuranceProductRepository { get; }
        IAccountNumberRepository AccountNumberRepository { get; }
        IClientRepository ClientRepository { get; }
        public Task CommitAsync();
        //public void Dispose();

    }
}
