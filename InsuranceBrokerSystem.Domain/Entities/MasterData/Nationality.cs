using System;

namespace InsuranceBrokerSystem.Domain.Entities.MasterData
{
    public class Nationality:BaseEntity
    {
        public string Name { get; set; }
        public string Code { get; set; } // Country code (e.g., SA, US, GB)
        public string Description { get; set; }
    }
}
