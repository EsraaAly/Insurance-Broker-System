namespace InsuranceBrokerSystem.Application.DTOs.Master_Table.Position
{
    public abstract class PositionBase
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public int Level { get; set; }
        public bool IsActive { get; set; } = true;
    }

    public class AddPositionDTO : PositionBase
    {
    }

    public class GetPositionDTO : PositionBase
    {
        public int Id { get; set; }
        public string CreatedBy { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool IsDeleted { get; set; }
        public override string ToString()
        {
            return Name;
        }
    }

    public class UpdatePositionDTO : PositionBase
    {
        public int Id { get; set; }
    }
}
