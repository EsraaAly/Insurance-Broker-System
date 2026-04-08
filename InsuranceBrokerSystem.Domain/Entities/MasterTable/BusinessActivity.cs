using System;

namespace InsuranceBrokerSystem.Domain.Entities.MasterTable
{
    public class BusinessActivity:BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Client> Clients { get; set; } = new HashSet<Client>();
    }
}
