using System.Windows;
using InsuranceBrokerSystem.UI.Services.Clients;
using InsuranceBrokerSystem.UI.Services.Financial;
using InsuranceBrokerSystem.UI.Services.Master_Table;

namespace InsuranceBrokerSystem.UI.Services
{
    public static class ApiConnectionTest
    {
        public static async Task<bool> TestAllConnections()
        {
            var testResults = new List<string>();
            bool allTestsPassed = true;

            // Test Master Table Services
            allTestsPassed &= await TestService("BusinessActivity", async () =>
            {
                var service = ServiceContainer.BusinessActivityApiService;
                var result = await service.GetAllBusinessActivitiesAsync();
                return result.Successed;
            }, testResults);

            allTestsPassed &= await TestService("InsuranceClass", async () =>
            {
                var service = ServiceContainer.InsuranceClassApiService;
                var result = await service.GetAllClassesAsync();
                return result.Successed;
            }, testResults);

            allTestsPassed &= await TestService("InsuranceCompany", async () =>
            {
                var service = ServiceContainer.InsuranceCompanyService;
                var result = await service.GetAllInsuranceCompaniesAsync();
                return result.Successed;
            }, testResults);

            allTestsPassed &= await TestService("InsuranceLOB", async () =>
            {
                var service = ServiceContainer.InsuranceLobApiService;
                var result = await service.GetAllLobsAsync();
                return result.Successed;
            }, testResults);

            allTestsPassed &= await TestService("Location", async () =>
            {
                var service = ServiceContainer.LocationApiService;
                var result = await service.GetAllLocationsAsync();
                return result.Successed;
            }, testResults);

            allTestsPassed &= await TestService("Nationality", async () =>
            {
                var service = ServiceContainer.NationalityApiService;
                var result = await service.GetAllNationalitiesAsync();
                return result.Successed;
            }, testResults);

            allTestsPassed &= await TestService("PolicyType", async () =>
            {
                var service = ServiceContainer.PolicyTypeApiService;
                var result = await service.GetAllPolicyTypesAsync();
                return result.Successed;
            }, testResults);

            allTestsPassed &= await TestService("SourceOfIncome", async () =>
            {
                var service = ServiceContainer.SourceOfIncomeApiService;
                var result = await service.GetAllSourceOfIncomesAsync();
                return result.Successed;
            }, testResults);

            // Test Client Services
            allTestsPassed &= await TestService("Client", async () =>
            {
                var service = ServiceContainer.ClientService;
                var result = await service.GetAllClientsAsync();
                return result.Successed;
            }, testResults);

            // Test Financial Services
            allTestsPassed &= await TestService("ChartOfAccount", async () =>
            {
                var service = ServiceContainer.ChartOfAccountApiService;
                var result = await service.LoadAccountsAsync();
                return result != null;
            }, testResults);

            // Display test results
            DisplayTestResults(testResults, allTestsPassed);

            return allTestsPassed;
        }

        private static async Task<bool> TestService(string serviceName, Func<Task<bool>> testAction, List<string> results)
        {
            try
            {
                var result = await testAction();
                if (result)
                {
                    results.Add($"[PASS] {serviceName} API connection successful");
                    return true;
                }
                else
                {
                    results.Add($"[FAIL] {serviceName} API returned failure response");
                    return false;
                }
            }
            catch (Exception ex)
            {
                results.Add($"[ERROR] {serviceName} API connection failed: {ex.Message}");
                return false;
            }
        }

        private static void DisplayTestResults(List<string> results, bool allPassed)
        {
            var message = string.Join(Environment.NewLine, results);
            var title = allPassed ? "API Connection Test Results - All Passed" : "API Connection Test Results - Some Failed";
            var icon = allPassed ? MessageBoxImage.Information : MessageBoxImage.Warning;

            MessageBox.Show(message, title, MessageBoxButton.OK, icon);
        }

        public static async Task<bool> TestIndividualService(string serviceName)
        {
            try
            {
                switch (serviceName)
                {
                    case "BusinessActivity":
                        var businessService = ServiceContainer.BusinessActivityApiService;
                        var businessResult = await businessService.GetAllBusinessActivitiesAsync();
                        return businessResult.Successed;

                    case "InsuranceClass":
                        var classService = ServiceContainer.InsuranceClassApiService;
                        var classResult = await classService.GetAllClassesAsync();
                        return classResult.Successed;

                    case "InsuranceCompany":
                        var companyService = ServiceContainer.InsuranceCompanyService;
                        var companyResult = await companyService.GetAllInsuranceCompaniesAsync();
                        return companyResult.Successed;

                    case "InsuranceLOB":
                        var lobService = ServiceContainer.InsuranceLobApiService;
                        var lobResult = await lobService.GetAllLobsAsync();
                        return lobResult.Successed;

                    case "Location":
                        var locationService = ServiceContainer.LocationApiService;
                        var locationResult = await locationService.GetAllLocationsAsync();
                        return locationResult.Successed;

                    case "Nationality":
                        var nationalityService = ServiceContainer.NationalityApiService;
                        var nationalityResult = await nationalityService.GetAllNationalitiesAsync();
                        return nationalityResult.Successed;

                    case "PolicyType":
                        var policyService = ServiceContainer.PolicyTypeApiService;
                        var policyResult = await policyService.GetAllPolicyTypesAsync();
                        return policyResult.Successed;

                    case "SourceOfIncome":
                        var sourceService = ServiceContainer.SourceOfIncomeApiService;
                        var sourceResult = await sourceService.GetAllSourceOfIncomesAsync();
                        return sourceResult.Successed;

                    case "Client":
                        var clientService = ServiceContainer.ClientService;
                        var clientResult = await clientService.GetAllClientsAsync();
                        return clientResult.Successed;

                    case "ChartOfAccount":
                        var accountService = ServiceContainer.ChartOfAccountApiService;
                        var accountResult = await accountService.LoadAccountsAsync();
                        return accountResult != null;

                    default:
                        MessageBox.Show($"Unknown service: {serviceName}", "Test Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error testing {serviceName}: {ex.Message}", "Test Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }
    }
}
