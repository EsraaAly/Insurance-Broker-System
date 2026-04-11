

using InsuranceBrokerSystem.UI.Interface;
using InsuranceBrokerSystem.UI.Services;
using System.Threading.Tasks;

namespace InsuranceBrokerSystem.UI.Views.MasterData
{
    public partial class BusinessActivityManagementWindow : Window
    {
        private readonly IServiceContainer _service;
        public ObservableCollection<GetBusinessActivityDTO> BusinessActivities { get; set; }
        public GetBusinessActivityDTO SelectedBusinessActivity { get; private set; }

        public BusinessActivityManagementWindow()
        {
            InitializeComponent();
            _service = new ServiceContainer(new HttpClientService());
            BusinessActivities = new ObservableCollection<GetBusinessActivityDTO>();
            lstBusinessActivities.ItemsSource = BusinessActivities;
            _ = LoadDataAsync();
        }

        private async Task LoadDataAsync()
        {
            try
            {
                BusinessActivities.Clear();
                var response = await _service.BusinessActivityApiService.GetAllBusinessActivitiesAsync();
                if (response.Successed && response.Data != null)
                {
                    foreach (var item in response.Data.OrderBy(x => x.ActivityName))
                    {
                        BusinessActivities.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading business activities: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void lstBusinessActivities_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedBusinessActivity = lstBusinessActivities.SelectedItem as GetBusinessActivityDTO;
            
            if (SelectedBusinessActivity != null)
            {
                txtName.Text = SelectedBusinessActivity.ActivityName;
                txtDescription.Text = SelectedBusinessActivity.Description;
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

            var newBusinessActivity = new AddBusinessActivityDTO
            {
                ActivityName = txtName.Text.Trim(),
                ActivityNameAr = txtName.Text.Trim(), // TODO: Add Arabic name field
                Description = txtDescription.Text.Trim()
            };

            try
            {
                var response = await _service.BusinessActivityApiService.AddBusinessActivityAsync(newBusinessActivity);
                if (response.Successed)
                {
                    ClearForm();
                    await LoadDataAsync(); // Refresh the list
                    MessageBox.Show("Business activity added successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show($"Error adding business activity: {response.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding business activity: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedBusinessActivity == null || string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Please select a business activity and enter a name.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var updateDto = new UpdateBusinessActivityDTO
            {
                Id = SelectedBusinessActivity.Id,
                ActivityName = txtName.Text.Trim(),
                ActivityNameAr = txtName.Text.Trim(), // TODO: Add Arabic name field
                Description = txtDescription.Text.Trim(),
                IsActive = SelectedBusinessActivity.IsActive
            };

            try
            {
                var response = await _service.BusinessActivityApiService.UpdateBusinessActivityAsync(updateDto);
                if (response.Successed)
                {
                    await LoadDataAsync(); // Refresh the list
                    MessageBox.Show("Business activity updated successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show($"Error updating business activity: {response.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating business activity: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedBusinessActivity == null)
            {
                MessageBox.Show("Please select a business activity to delete.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var result = MessageBox.Show($"Are you sure you want to delete '{SelectedBusinessActivity.ActivityName}'?", 
                "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    var response = await _service.BusinessActivityApiService.DeleteBusinessActivityAsync(SelectedBusinessActivity.Id);
                    if (response.Successed)
                    {
                        ClearForm();
                        await LoadDataAsync(); // Refresh the list
                        MessageBox.Show("Business activity deleted successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        MessageBox.Show($"Error deleting business activity: {response.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting business activity: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
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
            SelectedBusinessActivity = null;
            lstBusinessActivities.SelectedItem = null;
            btnAdd.IsEnabled = true;
            btnUpdate.IsEnabled = false;
            btnDelete.IsEnabled = false;
        }

        }
}
