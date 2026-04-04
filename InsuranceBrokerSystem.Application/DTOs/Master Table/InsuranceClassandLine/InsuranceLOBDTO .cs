
namespace InsuranceBrokerSystem.Application.DTOs.Master_Table.Insurance_Class_and_Line
{
    public class InsuranceLOBBase
    {
        public int ClassID { get; set; } // FK (string)

        public string LineOfBusiness { get; set; }
        public string Abbreviation { get; set; }
    } 
    public class AddInsuranceLOBDTO : InsuranceLOBBase
    {

    }
    public class GetInsuranceLOBDTO: InsuranceLOBBase
    {
        public int Id { get; set; }
        public string ClassName { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public override string ToString()
        {
            return LineOfBusiness;
        }
    }
    public class UpdateInsuranceLOBDTO: InsuranceLOBBase
    {
        public int Id { get; set; }
    }
}
