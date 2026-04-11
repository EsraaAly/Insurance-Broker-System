using InsuranceBrokerSystem.UI.Interface;
using InsuranceBrokerSystem.UI.Services;

namespace InsuranceBrokerSystem.UI.Views.MasterTable
{
    /// <summary>
    /// Interaction logic for InsuranceCompaniesList.xaml
    /// </summary>
    public partial class InsuranceCompaniesList : UserControl
    {
        private readonly IServiceContainer _service;
        public ObservableCollection<GetInsuranceCompanyDTO> InsuranceCompanies { get; set; } = new ObservableCollection<GetInsuranceCompanyDTO>();
        public InsuranceCompaniesList()
        {
            InitializeComponent();
            _service = new ServiceContainer(new HttpClientService());
            CompaniesGrid.ItemsSource = InsuranceCompanies;

        }
        private void Clear()
        {
            InsuranceCompanies.Clear();
            txtCompanyName.Text = "";
        }

        private void RegisterNewInsuranceCompany_Click(object sender, RoutedEventArgs e)
        {
            NewInsuranceCompany popup = new NewInsuranceCompany();
            popup.ShowDialog();
        }

        public async Task LoadInsuranceCompaniesDate()
        {
            try
            {
                InsuranceCompanies.Clear();
                var response = await _service.InsuranceCompanyService.GetAllInsuranceCompaniesAsync();

                if (response.Successed && response.Data != null)
                {
                    foreach (var company in response.Data)
                    {
                        InsuranceCompanies.Add(company);
                    }
                    CompaniesGrid.ItemsSource = InsuranceCompanies;
                }
                else
                {
                    CompaniesGrid.ItemsSource = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not connect to the Insurance API: {ex.Message}");
                CompaniesGrid.ItemsSource = null;
            }
        }
        public async Task LoadInsuranceCompaniesDateByName()
        {
            try
            {
                InsuranceCompanies.Clear();
                var response = await _service.InsuranceCompanyService.GetInsuranceCompanyByNameAsync(txtCompanyName.Text);
                
                if (response.Successed && response.Data != null)
                {
                    InsuranceCompanies.Add(response.Data);
                    CompaniesGrid.ItemsSource = InsuranceCompanies;
                }
                else
                {
                    CompaniesGrid.ItemsSource = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not connect to the Insurance API: {ex.Message}");
                CompaniesGrid.ItemsSource = null;
            }
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            Refresh();


        }
        private void Refresh()
        {
            if (txtCompanyName.Text.Length != 0)
            {
                LoadInsuranceCompaniesDateByName();
            }
            else
            {
                LoadInsuranceCompaniesDate();
            }
        }



        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            //this.Close();
        }

        private async void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            UpdateInsuranceCompanyDTO dto;
            // 2. Extract the data object from the CommandParameter
            var selectedRowData = button.CommandParameter as GetInsuranceCompanyDTO;
            if (selectedRowData != null)
            {
                  EditInsuranceCompany popup = new EditInsuranceCompany(selectedRowData.CompanyName);
                popup.ShowDialog();
                Refresh();
            }
        }
    }
}
