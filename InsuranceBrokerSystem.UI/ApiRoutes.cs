using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceBrokerSystem.UI
{
    public class ApiRoutes
    {
        public const string Version = "v1";

        public static class MasterTable
        {
            public static class BusinessActivity
            {
                public const string GetAllBusinessActivities = $"api/{Version}/BusinessActivity/GetAll";
                public const string AddBusinessActivity = $"api/{Version}/BusinessActivity/Add";
                public const string UpdateBusinessActivity = $"api/{Version}/BusinessActivity/Update";
                public const string DeleteBusinessActivity = $"api/{Version}/BusinessActivity/Delete";
                public const string GetBusinessActivityById = $"api/{Version}/BusinessActivity/GetById";
            }
            public static class InsuranceClass
            {
                public const string GetAllInsuranceClasses = $"api/{Version}/InsuranceClass/GetAll";
                public const string AddInsuranceClass = $"api/{Version}/InsuranceClass/Add";
                public const string UpdateInsuranceClass = $"api/{Version}/InsuranceClass/Update";
                public const string DeleteInsuranceClass = $"api/{Version}/InsuranceClass/Delete";

            }
            public static class InsuranceLOB
            {
                public const string GetAllInsuranceLOBs = $"api/{Version}/InsuranceLOB/GetAll";
                public const string GetLOBByClassIdAsync = $"api/{Version}/InsuranceLOB/GetByClassId";
                public const string AddInsuranceLOB = $"api/{Version}/InsuranceLOB/Add";
                public const string UpdateInsuranceLOB = $"api/{Version}/InsuranceLOB/Update";
                public const string DeleteInsuranceLOB = $"api/{Version}/InsuranceLOB/Delete";


            }

            public static class InsuranceComp
            {
                public const string GetAllInsuranceCompanies = $"api/{Version}/InsuranceComp/GetAll";
                public const string AddInsuranceComp = $"api/{Version}/InsuranceComp/Add";
                public const string GetInsuranceCompanyByName = $"api/{Version}/InsuranceComp/GetByName";
                public const string UpdateInsuranceComp = $"api/{Version}/InsuranceComp/Update";
                public const string DeleteInsuranceComp = $"api/{Version}/InsuranceComp/Delete";

            }
            public static class InsuranceCompProduct
            {
                public const string GetInsuranceProductByInsuranceId = $"api/{Version}/InsuranceCompProduct/GetByInsuranceId";
                
            }
            public static class InsuranceCompContact
            {
                public const string GetInsuranceContactByInsuranceIdAsync  = $"api/{Version}/InsuranceCompContact/GetByInsuranceId";
                
            }
            public static class Location
            {
                public const string GetAllLocations = $"api/{Version}/Location/GetAll";
                public const string AddLocation = $"api/{Version}/Location/Add";
                public const string UpdateLocation = $"api/{Version}/Location/Update";
                public const string DeleteLocation = $"api/{Version}/Location/Delete";
                public const string GetLocationById = $"api/{Version}/Location/GetById";
            }
            public static class Nationality
            {
                public const string GetAllNationalities = $"api/{Version}/Nationality/GetAll";
                public const string AddNationality = $"api/{Version}/Nationality/Add";
                public const string UpdateNationality = $"api/{Version}/Nationality/Update";
                public const string DeleteNationality = $"api/{Version}/Nationality/Delete";
                public const string GetNationalityById = $"api/{Version}/Nationality/GetById";
            }
            public static class PolicyType
            {
                public const string GetAllPolicyTypes = $"api/{Version}/PolicyType/GetAll";
                public const string AddPolicyType = $"api/{Version}/PolicyType/Add";
                public const string UpdatePolicyType = $"api/{Version}/PolicyType/Update";
                public const string DeletePolicyType = $"api/{Version}/PolicyType/Delete";
                public const string GetPolicyTypeById = $"api/{Version}/PolicyType/GetById";
            }
            public static class SourceOfIncome
            {
                public const string GetAllSourceOfIncomes = $"api/{Version}/SourceOfIncome/GetAll";
                public const string AddSourceOfIncome = $"api/{Version}/SourceOfIncome/Add";
                public const string UpdateSourceOfIncome = $"api/{Version}/SourceOfIncome/Update";
                public const string DeleteSourceOfIncome = $"api/{Version}/SourceOfIncome/Delete";
                public const string GetSourceOfIncomeById = $"api/{Version}/SourceOfIncome/GetById";
            }

        }
        public static class Financial
        {
            public static class Account
            {
                public const string AddAccount = $"api/{Version}/Account/Add";
                public const string UpdateAccount = $"api/{Version}/Account/Update";
                public const string GetAllAccounts = $"api/{Version}/Account/GetAll";
                public const string DeleteAccount = $"api/{Version}/Account/Delete";

            }
            public static class InsuranceComp
            {
                public const string ApproveInsuranceComp = $"api/{Version}/InsuranceComp/Approve";
                public const string RejectInsuranceComp = $"api/{Version}/InsuranceComp/Reject";
                public const string GenerateAccounts = $"api/{Version}/InsuranceComp/GenerateAccounts";
            }
        }
        public static class Clients
        {
            public const string GetAllClients = $"api/{Version}/Client/getAll";
            public const string GetClientById = $"api/{Version}/Client/getById";
            public const string AddClient = $"api/{Version}/Client/add";
            public const string UpdateClient = $"api/{Version}/Client/update";
            public const string DeleteClient = $"api/{Version}/Client/delete";
            public const string ApproveClient = $"api/{Version}/Client/approve";
            public const string RejectClient = $"api/{Version}/Client/reject";
            public const string BlockClient = $"api/{Version}/Client/block";
        }

    }
}
