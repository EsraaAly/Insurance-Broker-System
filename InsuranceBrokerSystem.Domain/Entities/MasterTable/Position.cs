using System;
using System.Collections.Generic;

namespace InsuranceBrokerSystem.Domain.Entities.MasterTable
{
    public class Position : BaseEntity
    {
        public string Name { get; set; }
        public string Code { get; set; } // Position code (e.g., MGR, EMP, DIR)
        public string Description { get; set; }
        public int Level { get; set; } // Hierarchy level (1=Executive, 2=Manager, 3=Staff, etc.)
        public bool IsActive { get; set; } = true;

        public virtual ICollection<ClientContact> ClientContacts { get; set; } = new HashSet<ClientContact>();
    }
}
