
namespace InsuranceBrokerSystem.Application.DTOs.Master_Table.Insurance_Class_and_Line
{
    public abstract class InsuranceClassBase
    {
        public string ClassName { get; set; }
        public string Abbreviation { get; set; }
    }

    public class AddInsuranceClassDTO : InsuranceClassBase
    {
    }

    public class GetInsuranceClassDTO : InsuranceClassBase
    {
        public int Id { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }

        // ADD THIS OVERRIDE:
        public override string ToString()
        {
            return ClassName;
        }
    }

    public class UpdateInsuranceClassDTO : InsuranceClassBase
    {
        public int Id { get; set; }
    }
}
