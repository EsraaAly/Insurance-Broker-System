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
                public const string GetInsuranceProductByInsuranceIdAsync = $"api/{Version}/InsuranceCompProduct/GetByInsuranceId";

            }
            public static class InsuranceCompContact
            {
                public const string GetInsuranceContactByInsuranceIdAsync = $"api/{Version}/InsuranceCompContact/GetByInsuranceId";

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
