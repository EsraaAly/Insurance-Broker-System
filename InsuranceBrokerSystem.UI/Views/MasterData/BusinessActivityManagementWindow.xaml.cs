

namespace InsuranceBrokerSystem.UI.Views.MasterData
{
    public partial class BusinessActivityManagementWindow : Window
    {
        public ObservableCollection<BusinessActivity> BusinessActivities { get; set; }
        public BusinessActivity SelectedBusinessActivity { get; private set; }

        private static List<BusinessActivity> _businessActivities = new List<BusinessActivity>
        {
            new BusinessActivity { Id = 1, Name = "Manufacturing", Description = "Manufacturing and production" },
            new BusinessActivity { Id = 2, Name = "Trading", Description = "Commercial trading" },
            new BusinessActivity { Id = 3, Name = "Services", Description = "Service industry" },
            new BusinessActivity { Id = 4, Name = "Construction", Description = "Construction and real estate" },
            new BusinessActivity { Id = 5, Name = "Technology", Description = "IT and technology services" }
        };

        public BusinessActivityManagementWindow()
        {
            InitializeComponent();
            BusinessActivities = new ObservableCollection<BusinessActivity>();
            lstBusinessActivities.ItemsSource = BusinessActivities;
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                BusinessActivities.Clear();
                foreach (var item in _businessActivities.OrderBy(x => x.Name))
                {
                    BusinessActivities.Add(item);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading business activities: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void lstBusinessActivities_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedBusinessActivity = lstBusinessActivities.SelectedItem as BusinessActivity;
            
            if (SelectedBusinessActivity != null)
            {
                txtName.Text = SelectedBusinessActivity.Name;
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

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Please enter a name.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var newBusinessActivity = new BusinessActivity
            {
                Name = txtName.Text.Trim(),
                Description = txtDescription.Text.Trim(),
                CreatedDate = DateTime.UtcNow,
                CreatedBy = "CurrentUser"
            };

            try
            {
                newBusinessActivity.Id = _businessActivities.Count > 0 ? _businessActivities.Max(x => x.Id) + 1 : 1;
                
                if (_businessActivities.Any(x => x.Name.Equals(newBusinessActivity.Name, StringComparison.OrdinalIgnoreCase)))
                {
                    MessageBox.Show("A business activity with this name already exists.", "Duplicate Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                _businessActivities.Add(newBusinessActivity);
                BusinessActivities.Add(newBusinessActivity);
                ClearForm();
                MessageBox.Show("Business activity added successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding business activity: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedBusinessActivity == null || string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Please select a business activity and enter a name.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            SelectedBusinessActivity.Name = txtName.Text.Trim();
            SelectedBusinessActivity.Description = txtDescription.Text.Trim();
            SelectedBusinessActivity.UpdatedDate = DateTime.UtcNow;
            SelectedBusinessActivity.UpdatedBy = "CurrentUser";

            try
            {
                if (_businessActivities.Any(x => x.Id != SelectedBusinessActivity.Id && x.Name.Equals(SelectedBusinessActivity.Name, StringComparison.OrdinalIgnoreCase)))
                {
                    MessageBox.Show("A business activity with this name already exists.", "Duplicate Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                var index = BusinessActivities.IndexOf(SelectedBusinessActivity);
                if (index >= 0)
                {
                    BusinessActivities[index] = SelectedBusinessActivity;
                }
                lstBusinessActivities.SelectedItem = SelectedBusinessActivity;
                MessageBox.Show("Business activity updated successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating business activity: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedBusinessActivity == null)
            {
                MessageBox.Show("Please select a business activity to delete.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var result = MessageBox.Show($"Are you sure you want to delete '{SelectedBusinessActivity.Name}'?", 
                "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    SelectedBusinessActivity.UpdatedDate = DateTime.UtcNow;
                    SelectedBusinessActivity.UpdatedBy = "CurrentUser";

                    BusinessActivities.Remove(SelectedBusinessActivity);
                    ClearForm();
                    MessageBox.Show("Business activity deleted successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
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
            LoadData();
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

        public static List<BusinessActivity> GetBusinessActivities()
        {
            return _businessActivities.OrderBy(x => x.Name).ToList();
        }
    }
}
