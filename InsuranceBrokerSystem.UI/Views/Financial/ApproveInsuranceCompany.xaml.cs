using InsuranceBrokerSystem.UI.Interface;
using InsuranceBrokerSystem.UI.Services;

namespace InsuranceBrokerSystem.UI.Views.Financial
{
    /// <summary>
    /// Interaction logic for ApproveInsuranceCompany.xaml
    /// </summary>
    public partial class ApproveInsuranceCompany : UserControl
    {
        private readonly IServiceContainer _service;
        public ObservableCollection<GetInsuranceCompanyDTO> InsuranceCompanies { get; set; } = new ObservableCollection<GetInsuranceCompanyDTO>();
        string status;
        public ApproveInsuranceCompany()
        {
            InitializeComponent();
            this.ChPending.IsChecked = true;
            status = ChPending.IsChecked == true ? "Pending" : "Rejected";
            _service = new ServiceContainer(new HttpClientService());
            CompaniesGrid.ItemsSource = InsuranceCompanies;
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            Refresh();
        }

        private void Refresh()
        {
            if (txtCompanyName.Text.Length != 0)
            {
                LoadInsuranceCompaniesDataByName();
            }
            else
            {
                LoadInsuranceCompaniesData();
            }
        }

        public async Task LoadInsuranceCompaniesData()
        {
            try
            {
                InsuranceCompanies.Clear();
                var result = await _service.InsuranceCompanyService.GetAllInsuranceCompaniesAsync();

                result.Data = result.Data.Where(c => c.Status == status).ToList();
                // Bind the result to your DataGrid
                CompaniesGrid.ItemsSource = result.Data;

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not connect to the Insurance API: {ex.Message}");
            }
            finally
            {

            }
        }
        public async Task LoadInsuranceCompaniesDataByName()
        {
            try
            {

                InsuranceCompanies.Clear();
                var result = await _service.InsuranceCompanyService.GetInsuranceCompanyByNameAsync(txtCompanyName.Text);
                //data = data.Where(c => c.Status == status).ToList();
                if (result.Data.State == status)
                {
                    InsuranceCompanies.Add(result.Data);
                }
                // Bind the result to your DataGrid
                //CompaniesGrid.ItemsSource = data;

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not connect to the Insurance API: {ex.Message}");
            }
            finally
            {

            }
        }

        private async void btnReject_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {
                var selectedCompany = button.CommandParameter as GetInsuranceCompanyDTO;
                if (selectedCompany != null)
                {
                    if (selectedCompany.Status == "Rejected")
                    {
                        MessageBox.Show($"Insurance company: {selectedCompany.CompanyName} is already Rejected.");
                        return;
                    }
                    await _service.InsuranceCompanyApprovalApiService.RejectInsuranceCompanyAsync(selectedCompany.Id);
                    MessageBox.Show($"Rejecting insurance company: {selectedCompany.CompanyName}");
                    Refresh();
                }
            }
        }

        private async void btnApprove_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {
                var selectedCompany = button.CommandParameter as GetInsuranceCompanyDTO;
                if (selectedCompany != null)
                {
                    await _service.InsuranceCompanyApprovalApiService.ApproveInsuranceCompanyAsync(selectedCompany.Id);
                    MessageBox.Show($"Approving insurance company and generating auto-accounts: {selectedCompany.CompanyName}");
                    Refresh();
                }
            }
        }
    }
}
