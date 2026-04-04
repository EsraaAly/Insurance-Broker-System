namespace InsuranceBrokerSystem.Application.Common.Interfaces.UnitOfWork
{
    public interface IUnitOfWork
    {

        IGenericRepository<InsuranceClass> GInsuranceClass { get; }
        //IGenericRepository<InsuranceLineOfBusiness> G_repoInsuranceLOB { get; }
        //IGenericRepository<InsuranceCompany> G_repoInsuranceCompany { get; }
        //IGenericRepository<InsuranceContact> G_repoInsuranceContract { get; }
        //IGenericRepository<InsuranceProduct> G_repoInsuranceProduct { get; }
        //IGenericRepository<Account> G_repoAccount { get; }

        //IRepoInsuranceClass repoInsuranceClass { get; }

        IRepoInsuranceLOB InsuranceLOB { get; }

        IRepoInsuranceCompany InsuranceCompany { get; }
        IRepoInsuranceContact InsuranceContract { get; }
        IRepoInsuranceProduct InsuranceProduct { get; }
        IRepoAccountNumber AccountNumber { get; }
        public Task CommitAsync();
        //public void Dispose();

    }
}
