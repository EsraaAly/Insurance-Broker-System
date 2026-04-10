using InsuranceBrokerSystem.UI.Services.Clients;
using InsuranceBrokerSystem.UI.Services.Financial;
using InsuranceBrokerSystem.UI.Services.Master_Table;

namespace InsuranceBrokerSystem.UI.Services
{
    public static class ServiceContainer
    {
        private static HttpClientService _httpClientService;
        private static ClientService _clientService;
        private static ChartOfAccountApiService _chartOfAccountApiService;
        private static InsuranceCompanyApprovalApiService _insuranceCompanyApprovalApiService;
        private static BusinessActivityApiService _businessActivityApiService;
        private static InsuranceClassApiService _insuranceClassApiService;
        private static InsuranceCompanyService _insuranceCompanyService;
        private static InsuranceLOBApiService _insuranceLobApiService;
        private static InsuranceContactService _insuranceContactService;
        private static InsuranceProductService _insuranceProductService;
        private static LocationApiService _locationApiService;
        private static NationalityApiService _nationalityApiService;
        private static PolicyTypeApiService _policyTypeApiService;
        private static SourceOfIncomeApiService _sourceOfIncomeApiService;

        public static HttpClientService HttpClientService => _httpClientService ??= new HttpClientService();
        public static ClientService ClientService => _clientService ??= new ClientService();
        public static ChartOfAccountApiService ChartOfAccountApiService => _chartOfAccountApiService ??= new ChartOfAccountApiService();
        public static InsuranceCompanyApprovalApiService InsuranceCompanyApprovalApiService => _insuranceCompanyApprovalApiService ??= new InsuranceCompanyApprovalApiService();
        public static BusinessActivityApiService BusinessActivityApiService => _businessActivityApiService ??= new BusinessActivityApiService();
        public static InsuranceClassApiService InsuranceClassApiService => _insuranceClassApiService ??= new InsuranceClassApiService();
        public static InsuranceCompanyService InsuranceCompanyService => _insuranceCompanyService ??= new InsuranceCompanyService();
        public static InsuranceLOBApiService InsuranceLobApiService => _insuranceLobApiService ??= new InsuranceLOBApiService();
        public static InsuranceContactService InsuranceContactService => _insuranceContactService ??= new InsuranceContactService();
        public static InsuranceProductService InsuranceProductService => _insuranceProductService ??= new InsuranceProductService();
        public static LocationApiService LocationApiService => _locationApiService ??= new LocationApiService();
        public static NationalityApiService NationalityApiService => _nationalityApiService ??= new NationalityApiService();
        public static PolicyTypeApiService PolicyTypeApiService => _policyTypeApiService ??= new PolicyTypeApiService();
        public static SourceOfIncomeApiService SourceOfIncomeApiService => _sourceOfIncomeApiService ??= new SourceOfIncomeApiService();
    }
}
