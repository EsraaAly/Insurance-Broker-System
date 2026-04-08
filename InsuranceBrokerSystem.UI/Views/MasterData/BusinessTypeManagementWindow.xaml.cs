using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using InsuranceBrokerSystem.Domain.Entities.MasterData;

namespace InsuranceBrokerSystem.UI.Views.MasterData
{
    public partial class BusinessTypeManagementWindow : Window
    {
        public ObservableCollection<BusinessType> BusinessTypes { get; set; }
        public BusinessType SelectedBusinessType { get; private set; }

        private static List<BusinessType> _businessTypes = new List<BusinessType>
        {
            new BusinessType { Id = 1, Name = "Governmental Entities", Description = "Government organizations" },
            new BusinessType { Id = 2, Name = "Semi-Government", Description = "Semi-government organizations" },
            new BusinessType { Id = 3, Name = "Publicly listed", Description = "Publicly traded companies" },
            new BusinessType { Id = 4, Name = "Corporation", Description = "Private corporations" },
            new BusinessType { Id = 5, Name = "SME", Description = "Small and Medium Enterprises" }
        };

        public BusinessTypeManagementWindow()
        {
            InitializeComponent();
            BusinessTypes = new ObservableCollection<BusinessType>();
            lstBusinessTypes.ItemsSource = BusinessTypes;
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                BusinessTypes.Clear();
                foreach (var item in _businessTypes.OrderBy(x => x.Name))
                {
                    BusinessTypes.Add(item);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading business types: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void lstBusinessTypes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedBusinessType = lstBusinessTypes.SelectedItem as BusinessType;
            
            if (SelectedBusinessType != null)
            {
                txtName.Text = SelectedBusinessType.Name;
                txtDescription.Text = SelectedBusinessType.Description;
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

            var newBusinessType = new BusinessType
            {
                Name = txtName.Text.Trim(),
                Description = txtDescription.Text.Trim(),
                CreatedAt = DateTime.UtcNow,
                CreatedBy = "CurrentUser"
            };

            try
            {
                newBusinessType.Id = _businessTypes.Count > 0 ? _businessTypes.Max(x => x.Id) + 1 : 1;
                
                if (_businessTypes.Any(x => x.Name.Equals(newBusinessType.Name, StringComparison.OrdinalIgnoreCase)))
                {
                    MessageBox.Show("A business type with this name already exists.", "Duplicate Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                _businessTypes.Add(newBusinessType);
                BusinessTypes.Add(newBusinessType);
                ClearForm();
                MessageBox.Show("Business type added successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding business type: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedBusinessType == null || string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Please select a business type and enter a name.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            SelectedBusinessType.Name = txtName.Text.Trim();
            SelectedBusinessType.Description = txtDescription.Text.Trim();
            SelectedBusinessType.UpdatedDate = DateTime.UtcNow;
            SelectedBusinessType.UpdatedBy = "CurrentUser";

            try
            {
                if (_businessTypes.Any(x => x.Id != SelectedBusinessType.Id && x.Name.Equals(SelectedBusinessType.Name, StringComparison.OrdinalIgnoreCase)))
                {
                    MessageBox.Show("A business type with this name already exists.", "Duplicate Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                var index = BusinessTypes.IndexOf(SelectedBusinessType);
                if (index >= 0)
                {
                    BusinessTypes[index] = SelectedBusinessType;
                }
                lstBusinessTypes.SelectedItem = SelectedBusinessType;
                MessageBox.Show("Business type updated successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating business type: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedBusinessType == null)
            {
                MessageBox.Show("Please select a business type to delete.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var result = MessageBox.Show($"Are you sure you want to delete '{SelectedBusinessType.Name}'?", 
                "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    SelectedBusinessType.IsActive = false;
                    SelectedBusinessType.UpdatedDate = DateTime.UtcNow;
                    SelectedBusinessType.UpdatedBy = "CurrentUser";

                    BusinessTypes.Remove(SelectedBusinessType);
                    ClearForm();
                    MessageBox.Show("Business type deleted successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting business type: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
            SelectedBusinessType = null;
            lstBusinessTypes.SelectedItem = null;
            btnAdd.IsEnabled = true;
            btnUpdate.IsEnabled = false;
            btnDelete.IsEnabled = false;
        }

        public static List<BusinessType> GetBusinessTypes()
        {
            return _businessTypes.OrderBy(x => x.Name).ToList();
        }
    }
}
