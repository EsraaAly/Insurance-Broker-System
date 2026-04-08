using System;

namespace InsuranceBrokerSystem.Domain.Entities.MasterTable
{
    public class Nationality:BaseEntity
    {
        public string Name { get; set; }
        public string Code { get; set; } // Country code (e.g., SA, US, GB)
        public string Description { get; set; }

        public virtual ICollection<Client> Clients { get; set; } = new HashSet<Client>();
    }
}
