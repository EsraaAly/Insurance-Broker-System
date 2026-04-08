using System;

namespace InsuranceBrokerSystem.Domain.Entities.MasterData
{
    public class Location:BaseEntity
    {
        public string Name { get; set; }
        public string Code { get; set; } // Location code
        public string Description { get; set; }
    }
}
