using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace InsuranceBrokerSystem.UI.Views.MasterData
{
    public partial class PolicyTypeManagementWindow : Window
    {
        public ObservableCollection<PolicyType> PolicyTypes { get; set; }
        public PolicyType SelectedPolicyType { get; private set; }

        // Mock data for demonstration - in real implementation, this would come from API/database
        private static List<PolicyType> _policyTypes = new List<PolicyType>
        {
            new PolicyType { Id = 1, Name = "Life Insurance", Description = "Life and health insurance policies" },
            new PolicyType { Id = 2, Name = "Vehicle Insurance", Description = "Car and vehicle insurance policies" },
            new PolicyType { Id = 3, Name = "Property Insurance", Description = "Home and property insurance policies" },
            new PolicyType { Id = 4, Name = "Travel Insurance", Description = "Travel and trip insurance policies" },
            new PolicyType { Id = 5, Name = "Business Insurance", Description = "Business and commercial insurance policies" }
        };

        public PolicyTypeManagementWindow()
        {
            InitializeComponent();
            PolicyTypes = new ObservableCollection<PolicyType>();
            lstPolicyTypes.ItemsSource = PolicyTypes;
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                PolicyTypes.Clear();
                foreach (var item in _policyTypes.OrderBy(x => x.Name))
                {
                    PolicyTypes.Add(item);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading policy types: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void lstPolicyTypes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedPolicyType = lstPolicyTypes.SelectedItem as PolicyType;
            
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

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Please enter a name.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var newPolicyType = new PolicyType
            {
                Name = txtName.Text.Trim(),
                Description = txtDescription.Text.Trim(),
                CreatedDate = DateTime.UtcNow,
                CreatedBy = "CurrentUser" // TODO: Get actual user
            };

            try
            {
                // Generate new ID
                newPolicyType.Id = _policyTypes.Count > 0 ? _policyTypes.Max(x => x.Id) + 1 : 1;
                
                // Check for duplicates
                if (_policyTypes.Any(x => x.Name.Equals(newPolicyType.Name, StringComparison.OrdinalIgnoreCase)))
                {
                    MessageBox.Show("A policy type with this name already exists.", "Duplicate Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                _policyTypes.Add(newPolicyType);
                PolicyTypes.Add(newPolicyType);
                ClearForm();
                MessageBox.Show("Policy type added successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding policy type: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedPolicyType == null || string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Please select a policy type and enter a name.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            SelectedPolicyType.Name = txtName.Text.Trim();
            SelectedPolicyType.Description = txtDescription.Text.Trim();
            SelectedPolicyType.UpdatedDate = DateTime.UtcNow;
            SelectedPolicyType.UpdatedBy = "CurrentUser"; // TODO: Get actual user

            try
            {
                // Check for duplicates (excluding current item)
                if (_policyTypes.Any(x => x.Id != SelectedPolicyType.Id && x.Name.Equals(SelectedPolicyType.Name, StringComparison.OrdinalIgnoreCase)))
                {
                    MessageBox.Show("A policy type with this name already exists.", "Duplicate Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                var index = PolicyTypes.IndexOf(SelectedPolicyType);
                if (index >= 0)
                {
                    PolicyTypes[index] = SelectedPolicyType;
                }
                lstPolicyTypes.SelectedItem = SelectedPolicyType;
                MessageBox.Show("Policy type updated successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating policy type: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
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
                    // Check if item is in use (in real implementation, check against clients/policies)
                    // For now, we'll mark as inactive instead of deleting
                    SelectedPolicyType.UpdatedDate = DateTime.UtcNow;
                    SelectedPolicyType.UpdatedBy = "CurrentUser";

                    PolicyTypes.Remove(SelectedPolicyType);
                    ClearForm();
                    MessageBox.Show("Policy type deleted successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
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
            SelectedPolicyType = null;
            lstPolicyTypes.SelectedItem = null;
            btnAdd.IsEnabled = true;
            btnUpdate.IsEnabled = false;
            btnDelete.IsEnabled = false;
        }

        // Static method to get policy types for combobox population
        public static List<PolicyType> GetPolicyTypes()
        {
            return _policyTypes.OrderBy(x => x.Name).ToList();
        }
    }
}
