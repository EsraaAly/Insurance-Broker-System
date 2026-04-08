using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using InsuranceBrokerSystem.Domain.Entities.MasterData;

namespace InsuranceBrokerSystem.UI.Views.MasterData
{
    public partial class RelationshipStatusManagementWindow : Window
    {
        public ObservableCollection<RelationshipStatus> RelationshipStatuses { get; set; }
        public RelationshipStatus SelectedRelationshipStatus { get; private set; }

        private static List<RelationshipStatus> _relationshipStatuses = new List<RelationshipStatus>
        {
            new RelationshipStatus { Id = 1, Name = "Single", Description = "Not married" },
            new RelationshipStatus { Id = 2, Name = "Married", Description = "Currently married" },
            new RelationshipStatus { Id = 3, Name = "Divorced", Description = "Legally divorced" },
            new RelationshipStatus { Id = 4, Name = "Widowed", Description = "Spouse has passed away" },
            new RelationshipStatus { Id = 5, Name = "Separated", Description = "Legally separated" }
        };

        public RelationshipStatusManagementWindow()
        {
            InitializeComponent();
            RelationshipStatuses = new ObservableCollection<RelationshipStatus>();
            lstRelationshipStatuses.ItemsSource = RelationshipStatuses;
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                RelationshipStatuses.Clear();
                foreach (var item in _relationshipStatuses.OrderBy(x => x.Name))
                {
                    RelationshipStatuses.Add(item);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading relationship statuses: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void lstRelationshipStatuses_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedRelationshipStatus = lstRelationshipStatuses.SelectedItem as RelationshipStatus;
            
            if (SelectedRelationshipStatus != null)
            {
                txtName.Text = SelectedRelationshipStatus.Name;
                txtDescription.Text = SelectedRelationshipStatus.Description;
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

            var newRelationshipStatus = new RelationshipStatus
            {
                Name = txtName.Text.Trim(),
                Description = txtDescription.Text.Trim(),
                CreatedDate = DateTime.UtcNow,
                CreatedBy = "CurrentUser"
            };

            try
            {
                newRelationshipStatus.Id = _relationshipStatuses.Count > 0 ? _relationshipStatuses.Max(x => x.Id) + 1 : 1;
                
                if (_relationshipStatuses.Any(x => x.Name.Equals(newRelationshipStatus.Name, StringComparison.OrdinalIgnoreCase)))
                {
                    MessageBox.Show("A relationship status with this name already exists.", "Duplicate Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                _relationshipStatuses.Add(newRelationshipStatus);
                RelationshipStatuses.Add(newRelationshipStatus);
                ClearForm();
                MessageBox.Show("Relationship status added successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding relationship status: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedRelationshipStatus == null || string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Please select a relationship status and enter a name.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            SelectedRelationshipStatus.Name = txtName.Text.Trim();
            SelectedRelationshipStatus.Description = txtDescription.Text.Trim();
            SelectedRelationshipStatus.UpdatedDate = DateTime.UtcNow;
            SelectedRelationshipStatus.UpdatedBy = "CurrentUser";

            try
            {
                if (_relationshipStatuses.Any(x => x.Id != SelectedRelationshipStatus.Id && x.Name.Equals(SelectedRelationshipStatus.Name, StringComparison.OrdinalIgnoreCase)))
                {
                    MessageBox.Show("A relationship status with this name already exists.", "Duplicate Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                var index = RelationshipStatuses.IndexOf(SelectedRelationshipStatus);
                if (index >= 0)
                {
                    RelationshipStatuses[index] = SelectedRelationshipStatus;
                }
                lstRelationshipStatuses.SelectedItem = SelectedRelationshipStatus;
                MessageBox.Show("Relationship status updated successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating relationship status: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedRelationshipStatus == null)
            {
                MessageBox.Show("Please select a relationship status to delete.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var result = MessageBox.Show($"Are you sure you want to delete '{SelectedRelationshipStatus.Name}'?", 
                "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    SelectedRelationshipStatus.UpdatedDate = DateTime.UtcNow;
                    SelectedRelationshipStatus.UpdatedBy = "CurrentUser";

                    RelationshipStatuses.Remove(SelectedRelationshipStatus);
                    ClearForm();
                    MessageBox.Show("Relationship status deleted successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting relationship status: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
            SelectedRelationshipStatus = null;
            lstRelationshipStatuses.SelectedItem = null;
            btnAdd.IsEnabled = true;
            btnUpdate.IsEnabled = false;
            btnDelete.IsEnabled = false;
        }

        public static List<RelationshipStatus> GetRelationshipStatuses()
        {
            return _relationshipStatuses.OrderBy(x => x.Name).ToList();
        }
    }
}
