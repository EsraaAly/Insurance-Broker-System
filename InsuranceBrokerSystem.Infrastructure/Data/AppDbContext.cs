
using InsuranceBrokerSystem.Domain.Entities.Financial;

namespace InsuranceBrokerSystem.Infrastructure.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<InsuranceClass> InsuranceClasses { get; set; }
        public DbSet<InsuranceLineOfBusiness> InsuranceLines { get; set; }

        public DbSet<InsuranceCompany> InsuranceCompanies { get; set; }
        public DbSet<InsuranceContact> InsuranceContacts { get; set; }
        public DbSet<InsuranceProduct> InsuranceProducts { get; set; }

        public DbSet<PolicyType> policyTypes { get; set; }
        public DbSet<Nationality> nationalities { get; set; }
        public DbSet<BusinessActivity> businessActivities { get; set; }

        public DbSet<Account> accounts { get; set; }

        public DbSet<Domain.Entities.Client.Client> Clients { get; set; }
        public DbSet<ClientContact> ClientContacts { get; set; }
        public DbSet<ClientDocument> ClientDocuments { get; set; }
        public DbSet<ClientBankAccount> ClientBankAccounts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<TEntity>().HasQueryFilter(i => !i.IsDeleted)

            #region "InsuranceClass"
            modelBuilder.Entity<InsuranceClass>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<InsuranceLineOfBusiness>()
                .HasKey(x => x.Id);

            // Relationship (ClassName string FK)
            modelBuilder.Entity<InsuranceLineOfBusiness>()
                .HasOne(l => l.InsuranceClass)
                .WithMany(c => c.Lines)
                .HasForeignKey(l => l.ClassID)
                .HasPrincipalKey(c => c.Id);
            #endregion
            
            #region "PolicyType"
            modelBuilder.Entity<PolicyType>()
                .HasKey(x => x.Id);
            #endregion

            #region "Nationality"
            modelBuilder.Entity <Nationality>()
                .HasKey(x => x.Id);
            #endregion

            #region "BusinessActivity"
            modelBuilder.Entity<BusinessActivity>()
                .HasKey(x => x.Id);
            #endregion

            #region "InsuranceCompany"
            modelBuilder.Entity<InsuranceCompany>()
                .HasKey(x => x.Id);
            modelBuilder.Entity<InsuranceCompany>()
                .HasIndex(x => x.CompanyName)
                .IsUnique();
            modelBuilder.Entity<InsuranceCompany>()
                .HasIndex(x => x.CRNo)
                .IsUnique();
            modelBuilder.Entity<InsuranceProduct>()
                .HasKey(x => x.Id);
            modelBuilder.Entity<InsuranceContact>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<InsuranceCompany>()
                .HasMany(c => c.Products)
                .WithOne(p => p.InsuranceCompany)
                .HasForeignKey(p => p.InsuranceCompanyId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<InsuranceCompany>()
                .HasMany(c => c.Contacts)
                .WithOne(p => p.InsuranceCompany)
                .HasForeignKey(p => p.InsuranceCompanyId)
                .OnDelete(DeleteBehavior.Cascade);
            #endregion

            #region "Account"
            modelBuilder.Entity<Account>()
                .HasKey(x => x.Id);
            modelBuilder.Entity<Account>()
                .HasIndex(x => x.AccountNumber)
                .HasFilter("[IsDeleted] = 0")
                .IsUnique();
            modelBuilder.Entity<Account>()
                .HasIndex(x => x.AccountName)
                .HasFilter("[IsDeleted] = 0")
                .IsUnique();
            modelBuilder.Entity<Account>()
                .HasOne(p=>p.Parent)
                .WithMany(c=>c.Children)
                .HasForeignKey(p=>p.ParentId)
                .OnDelete(DeleteBehavior.Restrict);
            #endregion

            #region "Client"
            modelBuilder.Entity<Domain.Entities.Client.Client>()
                .HasKey(x => x.Id);
            modelBuilder.Entity<Domain.Entities.Client.Client>()
                .HasIndex(x => x.ClientName)
                .HasFilter("[IsDeleted] = 0")
                .IsUnique();

            modelBuilder.Entity<Domain.Entities.Client.Client>(entity =>
            {
                entity.Property(e => e.ClientName).IsRequired().HasMaxLength(200);
                entity.Property(e => e.ClientNameAr).IsRequired().HasMaxLength(200);

                entity.Property(e => e.IdentityNo).HasMaxLength(15);
                entity.Property(e => e.Nationality).HasMaxLength(50);

                entity.Property(e => e.CommercialRegistrationNo).HasMaxLength(20);
                entity.Property(e => e.VATNo).HasMaxLength(15);
            });
            modelBuilder.Entity<Client>()
            .ToTable(t => t.HasCheckConstraint("CK_Client_Validation",
        "(ClientType = 1 AND IdentityNo IS NOT NULL) OR (ClientType = 2 AND CommercialRegistrationNo IS NOT NULL)"));
            modelBuilder.Entity<ClientContact>()
            .HasKey(x => x.Id);
            modelBuilder.Entity<ClientDocument>()
                .HasKey(x => x.Id);
            modelBuilder.Entity<ClientBankAccount>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<Domain.Entities.Client.Client>()
                .HasMany(c => c.Contacts)
                .WithOne(p => p.Client)
                .HasForeignKey(p => p.ClientId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Domain.Entities.Client.Client>()
                .HasMany(c => c.Documents)
                .WithOne(p => p.Client)
                .HasForeignKey(p => p.ClientId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Domain.Entities.Client.Client>()
                .HasMany(c => c.BankAccounts)
                .WithOne(p => p.Client)
                .HasForeignKey(p => p.ClientId)
                .OnDelete(DeleteBehavior.Cascade);
            #endregion
        }
    }
}
