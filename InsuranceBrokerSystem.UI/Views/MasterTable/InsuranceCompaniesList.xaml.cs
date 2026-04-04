
namespace InsuranceBrokerSystem.UI.Views.MasterTable
{
    /// <summary>
    /// Interaction logic for InsuranceCompaniesList.xaml
    /// </summary>
    public partial class InsuranceCompaniesList : UserControl
    {
        private readonly InsuranceCompanyService _InsuranceCompanyService;
        public ObservableCollection<GetInsuranceCompanyDTO> InsuranceCompanies { get; set; } = new ObservableCollection<GetInsuranceCompanyDTO>();
        public InsuranceCompaniesList()
        {
            InitializeComponent();
            _InsuranceCompanyService = new InsuranceCompanyService();
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
                var data = await _InsuranceCompanyService.GetAllInsuranceCompaniesAsync();

                // Bind the result to your DataGrid
                CompaniesGrid.ItemsSource = data;

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not connect to the Insurance API: {ex.Message}");
            }
            finally
            {

            }
        }
        public async Task LoadInsuranceCompaniesDateByName()
        {
            try
            {

                InsuranceCompanies.Clear();
                var data = await _InsuranceCompanyService.GetInsuranceCompanyByNameAsync(txtCompanyName.Text);
                if (data != null)
                {
                    InsuranceCompanies.Add(data);
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
