using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using InsuranceBrokerSystem.Domain.Entities.MasterData;

namespace InsuranceBrokerSystem.UI.Views.MasterData
{
    public partial class SourceOfIncomeManagementWindow : Window
    {
        public ObservableCollection<SourceOfIncome> SourcesOfIncome { get; set; }
        public SourceOfIncome SelectedSourceOfIncome { get; private set; }

        private static List<SourceOfIncome> _sourcesOfIncome = new List<SourceOfIncome>
        {
            new SourceOfIncome { Id = 1, Name = "Salary", Description = "Regular employment income" },
            new SourceOfIncome { Id = 2, Name = "Business", Description = "Business income" },
            new SourceOfIncome { Id = 3, Name = "Investment", Description = "Investment returns" },
            new SourceOfIncome { Id = 4, Name = "Rental", Description = "Property rental income" },
            new SourceOfIncome { Id = 5, Name = "Pension", Description = "Retirement pension" }
        };

        public SourceOfIncomeManagementWindow()
        {
            InitializeComponent();
            SourcesOfIncome = new ObservableCollection<SourceOfIncome>();
            lstSourcesOfIncome.ItemsSource = SourcesOfIncome;
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                SourcesOfIncome.Clear();
                foreach (var item in _sourcesOfIncome.OrderBy(x => x.Name))
                {
                    SourcesOfIncome.Add(item);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading sources of income: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void lstSourcesOfIncome_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedSourceOfIncome = lstSourcesOfIncome.SelectedItem as SourceOfIncome;
            
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

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Please enter a name.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var newSourceOfIncome = new SourceOfIncome
            {
                Name = txtName.Text.Trim(),
                Description = txtDescription.Text.Trim(),
                CreatedDate = DateTime.UtcNow,
                CreatedBy = "CurrentUser"
            };

            try
            {
                newSourceOfIncome.Id = _sourcesOfIncome.Count > 0 ? _sourcesOfIncome.Max(x => x.Id) + 1 : 1;
                
                if (_sourcesOfIncome.Any(x => x.Name.Equals(newSourceOfIncome.Name, StringComparison.OrdinalIgnoreCase)))
                {
                    MessageBox.Show("A source of income with this name already exists.", "Duplicate Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                _sourcesOfIncome.Add(newSourceOfIncome);
                SourcesOfIncome.Add(newSourceOfIncome);
                ClearForm();
                MessageBox.Show("Source of income added successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding source of income: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedSourceOfIncome == null || string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Please select a source of income and enter a name.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            SelectedSourceOfIncome.Name = txtName.Text.Trim();
            SelectedSourceOfIncome.Description = txtDescription.Text.Trim();
            SelectedSourceOfIncome.UpdatedDate = DateTime.UtcNow;
            SelectedSourceOfIncome.UpdatedBy = "CurrentUser";

            try
            {
                if (_sourcesOfIncome.Any(x => x.Id != SelectedSourceOfIncome.Id && x.Name.Equals(SelectedSourceOfIncome.Name, StringComparison.OrdinalIgnoreCase)))
                {
                    MessageBox.Show("A source of income with this name already exists.", "Duplicate Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                var index = SourcesOfIncome.IndexOf(SelectedSourceOfIncome);
                if (index >= 0)
                {
                    SourcesOfIncome[index] = SelectedSourceOfIncome;
                }
                lstSourcesOfIncome.SelectedItem = SelectedSourceOfIncome;
                MessageBox.Show("Source of income updated successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating source of income: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedSourceOfIncome == null)
            {
                MessageBox.Show("Please select a source of income to delete.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var result = MessageBox.Show($"Are you sure you want to delete '{SelectedSourceOfIncome.Name}'?", 
                "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    SelectedSourceOfIncome.UpdatedDate = DateTime.UtcNow;
                    SelectedSourceOfIncome.UpdatedBy = "CurrentUser";

                    SourcesOfIncome.Remove(SelectedSourceOfIncome);
                    ClearForm();
                    MessageBox.Show("Source of income deleted successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting source of income: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
            SelectedSourceOfIncome = null;
            lstSourcesOfIncome.SelectedItem = null;
            btnAdd.IsEnabled = true;
            btnUpdate.IsEnabled = false;
            btnDelete.IsEnabled = false;
        }

        public static List<SourceOfIncome> GetSourcesOfIncome()
        {
            return _sourcesOfIncome.OrderBy(x => x.Name).ToList();
        }
    }
}
