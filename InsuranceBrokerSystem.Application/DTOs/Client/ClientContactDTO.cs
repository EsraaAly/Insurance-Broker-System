
using InsuranceBrokerSystem.Application.DTOs.Master_Table.Position;

namespace InsuranceBrokerSystem.Application.DTOs.Client
{
    public abstract class ClientContactBase
    {
        public int    ClientId      { get; set; }
        public int?   PositionId    { get; set; }
        public string Name          { get; set; } = string.Empty;
        public string Extension     { get; set; } = string.Empty;
        public string Mobile        { get; set; } = string.Empty;
        public string Tele          { get; set; } = string.Empty;
        public string Email         { get; set; } = string.Empty;
        public bool   SaddadInvoice { get; set; }
        public string? Branch       { get; set; }
    }

    public class AddClientContactDTO : ClientContactBase
    {
    }

    public class GetClientContactDTO : ClientContactBase
    {
        public int Id { get; set; }

        public string CreatedBy     { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }
        public string? UpdatedBy   { get; set; }
        public DateTime? UpdatedDate { get; set; }
        
        // Navigation property for Position information
        public GetPositionDTO? Position { get; set; }
    }

    public class UpdateClientContactDTO : ClientContactBase
    {
        public int Id { get; set; }
    }
}
