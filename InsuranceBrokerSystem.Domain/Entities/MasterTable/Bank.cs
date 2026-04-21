using System;
using System.Collections.Generic;

namespace InsuranceBrokerSystem.Domain.Entities.MasterTable
{
    public class Bank : BaseEntity
    {
        public string Name { get; set; }
        public string Code { get; set; } // Bank code (e.g., SA01, RJH01)
        public string Description { get; set; }
        public string SwiftCode { get; set; } // SWIFT/BIC code
        public bool IsActive { get; set; } = true;

        public virtual ICollection<ClientBankAccount> ClientBankAccounts { get; set; } = new HashSet<ClientBankAccount>();
    }
}
