using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using InsuranceBrokerSystem.UI.Interface;
using InsuranceBrokerSystem.UI.Services;

namespace InsuranceBrokerSystem.UI.Views.MasterData
{
    public partial class PolicyTypeManagementWindow : Window
    {
        private readonly IServiceContainer _service;
        public ObservableCollection<GetPolicyTypeDTO> PolicyTypes { get; set; }
        public GetPolicyTypeDTO SelectedPolicyType { get; private set; }

        public PolicyTypeManagementWindow()
        {
            InitializeComponent();
            _service = new ServiceContainer(new HttpClientService());
            PolicyTypes = new ObservableCollection<GetPolicyTypeDTO>();
            lstPolicyTypes.ItemsSource = PolicyTypes;
            _ = LoadDataAsync();
        }

        private async Task LoadDataAsync()
        {
            try
            {
                PolicyTypes.Clear();
                var response = await _service.PolicyTypeApiService.GetAllPolicyTypesAsync();
                if (response.Successed && response.Data != null)
                {
                    foreach (var item in response.Data.OrderBy(x => x.Name))
                    {
                        PolicyTypes.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading policy types: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void lstPolicyTypes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedPolicyType = lstPolicyTypes.SelectedItem as GetPolicyTypeDTO;
            
            if (SelectedPolicyType != null)
            {
                txtName.Text = SelectedPolicyType.Name;
                txtDescription.Text = SelectedPolicyType.Description;
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

            var newPolicyType = new AddPolicyTypeDTO
            {
                Name = txtName.Text.Trim(),
                Description = txtDescription.Text.Trim()
            };

            try
            {
                var response = await _service.PolicyTypeApiService.AddPolicyTypeAsync(newPolicyType);
                
                if (response.Successed)
                {
                    await LoadDataAsync(); // Reload data from API
                    ClearForm();
                    MessageBox.Show("Policy type added successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show($"Failed to add policy type: {response.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding policy type: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedPolicyType == null)
            {
                MessageBox.Show("Please select a policy type to delete.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var result = MessageBox.Show($"Are you sure you want to delete '{SelectedPolicyType.Name}'?", 
                "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    var response = await _service.PolicyTypeApiService.DeletePolicyTypeAsync(SelectedPolicyType.Id);
                    
                    if (response.Successed)
                    {
                        await LoadDataAsync(); // Reload data from API
                        ClearForm();
                        MessageBox.Show("Policy type deleted successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        MessageBox.Show($"Failed to delete policy type: {response.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting policy type: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            ClearForm();
        }

        private async void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            await LoadDataAsync();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ClearForm()
        {
            txtName.Clear();
            txtDescription.Clear();
            SelectedPolicyType = null;
            lstPolicyTypes.SelectedItem = null;
            btnAdd.IsEnabled = true;
            btnUpdate.IsEnabled = false;
            btnDelete.IsEnabled = false;
        }

        private async void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedPolicyType == null || string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Please select a policy type and enter a name.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var updatePolicyType = new UpdatePolicyTypeDTO
            {
                Id = SelectedPolicyType.Id,
                Name = txtName.Text.Trim(),
                Description = txtDescription.Text.Trim()
            };

            try
            {
                var response = await _service.PolicyTypeApiService.UpdatePolicyTypeAsync(updatePolicyType);

                if (response.Successed)
                {
                    await LoadDataAsync(); // Reload data from API
                    ClearForm();
                    MessageBox.Show("Policy type updated successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show($"Failed to update policy type: {response.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating policy type: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Static method to get policy types for combobox population
        //public static List<PolicyType> GetPolicyTypes()
        //{
        //    return PolicyTypes.OrderBy(x => x.Name).ToList();
        //}
    }
}
