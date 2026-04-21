

using InsuranceBrokerSystem.UI.Interface;
using InsuranceBrokerSystem.UI.Services;
using System.Threading.Tasks;

namespace InsuranceBrokerSystem.UI.Views.MasterData
{
    public partial class SourceOfIncomeManagementWindow : Window
    {
        private readonly IServiceContainer _service;
        public ObservableCollection<GetSourceOfIncomeDTO> SourcesOfIncome { get; set; }
        public GetSourceOfIncomeDTO SelectedSourceOfIncome { get; private set; }

        public SourceOfIncomeManagementWindow()
        {
            InitializeComponent();
            _service = new ServiceContainer(new HttpClientService());
            SourcesOfIncome = new ObservableCollection<GetSourceOfIncomeDTO>();
            lstSourcesOfIncome.ItemsSource = SourcesOfIncome;
            _ = LoadDataAsync();
        }

        private async Task LoadDataAsync()
        {
            try
            {
                SourcesOfIncome.Clear();
                var response = await _service.SourceOfIncomeApiService.GetAllSourceOfIncomesAsync();
                if (response.Successed && response.Data != null)
                {
                    foreach (var item in response.Data.OrderBy(x => x.Name))
                    {
                        SourcesOfIncome.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading sources of income: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void lstSourcesOfIncome_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedSourceOfIncome = lstSourcesOfIncome.SelectedItem as GetSourceOfIncomeDTO;
            
            if (SelectedSourceOfIncome != null)
            {
                txtName.Text = SelectedSourceOfIncome.Name;
                txtDescription.Text = SelectedSourceOfIncome.Description;
                btnUpdate.IsEnabled = true;
                btnDelete.IsEnabled = true;
                btnAdd.IsEnabled = false;
            }
            else
            {
                ClearForm();
            }
        }

        private async void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Please enter a name.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var newSourceOfIncome = new AddSourceOfIncomeDTO
            {
                Name = txtName.Text.Trim(),
                Description = txtDescription.Text.Trim()
            };

            try
            {
                var response = await _service.SourceOfIncomeApiService.AddSourceOfIncomeAsync(newSourceOfIncome);
                if (response.Successed)
                {
                    ClearForm();
                    await LoadDataAsync(); // Refresh the list
                    MessageBox.Show("Source of income added successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show($"Error adding source of income: {response.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding source of income: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Implement update functionality using API service
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Implement delete functionality using API service
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            ClearForm();
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            LoadDataAsync();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ClearForm()
        {
            txtName.Clear();
            txtDescription.Clear();
            SelectedSourceOfIncome = null;
            lstSourcesOfIncome.SelectedItem = null;
            btnAdd.IsEnabled = true;
            btnUpdate.IsEnabled = false;
            btnDelete.IsEnabled = false;
        }

    }
}
