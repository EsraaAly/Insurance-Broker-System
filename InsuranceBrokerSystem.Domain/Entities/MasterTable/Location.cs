using System;

namespace InsuranceBrokerSystem.Domain.Entities.MasterTable
{
    public class Location:BaseEntity
    {
        public string Name { get; set; }
        public string Code { get; set; } // Location code
        public string Description { get; set; }

        public virtual ICollection<Client> Clients { get; set; } = new HashSet<Client>();
    }
}
