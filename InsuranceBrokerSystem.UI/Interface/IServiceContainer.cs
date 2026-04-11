using InsuranceBrokerSystem.UI.Services;
using InsuranceBrokerSystem.UI.Services.Clients;
using InsuranceBrokerSystem.UI.Services.Financial;
using InsuranceBrokerSystem.UI.Services.Master_Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceBrokerSystem.UI.Interface
{
    public interface IServiceContainer
    {
         ClientService ClientService{ get; }
         BusinessActivityApiService BusinessActivityApiService{ get; }
         ChartOfAccountApiService ChartOfAccountApiService{ get; }
         InsuranceCompanyApprovalApiService InsuranceCompanyApprovalApiService{ get; }
         InsuranceClassApiService InsuranceClassApiService{ get; }
         InsuranceCompanyService InsuranceCompanyService{ get; }
         InsuranceLOBApiService InsuranceLobApiService{ get; }
         InsuranceContactService InsuranceContactService{ get; }
         InsuranceProductService InsuranceProductService{ get; }
         LocationApiService LocationApiService{ get; }
         NationalityApiService NationalityApiService{ get; }
         PolicyTypeApiService PolicyTypeApiService{ get; }
         SourceOfIncomeApiService SourceOfIncomeApiService{ get; }

    }
}
