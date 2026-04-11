using InsuranceBrokerSystem.UI.Interface;
using InsuranceBrokerSystem.UI.Services.Clients;
using InsuranceBrokerSystem.UI.Services.Financial;
using InsuranceBrokerSystem.UI.Services.Master_Table;

namespace InsuranceBrokerSystem.UI.Services
{
    public class ServiceContainer : IServiceContainer
    {
        private HttpClientService _httpClientService;
        private ClientService _clientService;
        private ChartOfAccountApiService _chartOfAccountApiService;
        private InsuranceCompanyApprovalApiService _insuranceCompanyApprovalApiService;
        private BusinessActivityApiService _businessActivityApiService;
        private InsuranceClassApiService _insuranceClassApiService;
        private InsuranceCompanyService _insuranceCompanyService;
        private InsuranceLOBApiService _insuranceLobApiService;
        private InsuranceContactService _insuranceContactService;
        private InsuranceProductService _insuranceProductService;
        private LocationApiService _locationApiService;
        private NationalityApiService _nationalityApiService;
        private PolicyTypeApiService _policyTypeApiService;
        private SourceOfIncomeApiService _sourceOfIncomeApiService;

        public ServiceContainer(HttpClientService httpClientService)
        {
            _httpClientService = httpClientService;
        }
        public ClientService ClientService => _clientService ??= new ClientService(_httpClientService);
        public ChartOfAccountApiService ChartOfAccountApiService => _chartOfAccountApiService ??= new ChartOfAccountApiService(_httpClientService);
        public InsuranceCompanyApprovalApiService InsuranceCompanyApprovalApiService => _insuranceCompanyApprovalApiService ??= new InsuranceCompanyApprovalApiService(_httpClientService);
        public BusinessActivityApiService BusinessActivityApiService => _businessActivityApiService ??= new BusinessActivityApiService(_httpClientService);
        public InsuranceClassApiService InsuranceClassApiService => _insuranceClassApiService ??= new InsuranceClassApiService(_httpClientService);
        public InsuranceCompanyService InsuranceCompanyService => _insuranceCompanyService ??= new InsuranceCompanyService(_httpClientService);
        public InsuranceLOBApiService InsuranceLobApiService => _insuranceLobApiService ??= new InsuranceLOBApiService(_httpClientService);
        public InsuranceContactService InsuranceContactService => _insuranceContactService ??= new InsuranceContactService(_httpClientService);
        public InsuranceProductService InsuranceProductService => _insuranceProductService ??= new InsuranceProductService(_httpClientService);
        public LocationApiService LocationApiService => _locationApiService ??= new LocationApiService(_httpClientService);
        public NationalityApiService NationalityApiService => _nationalityApiService ??= new NationalityApiService(_httpClientService);
        public PolicyTypeApiService PolicyTypeApiService => _policyTypeApiService ??= new PolicyTypeApiService(_httpClientService);
        public SourceOfIncomeApiService SourceOfIncomeApiService => _sourceOfIncomeApiService ??= new SourceOfIncomeApiService(_httpClientService);

    }
}
