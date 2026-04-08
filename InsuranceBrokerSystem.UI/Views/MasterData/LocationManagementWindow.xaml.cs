using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using InsuranceBrokerSystem.Domain.Entities.MasterData;

namespace InsuranceBrokerSystem.UI.Views.MasterData
{
    public partial class LocationManagementWindow : Window
    {
        public ObservableCollection<Location> Locations { get; set; }
        public Location SelectedLocation { get; private set; }

        private static List<Location> _locations = new List<Location>
        {
            new Location { Id = 1, Name = "Riyadh", Code = "RYD", Description = "Capital city of Saudi Arabia" },
            new Location { Id = 2, Name = "Jeddah", Code = "JED", Description = "Major port city" },
            new Location { Id = 3, Name = "Dammam", Code = "DMM", Description = "Eastern Province city" },
            new Location { Id = 4, Name = "Mecca", Code = "MEC", Description = "Holy city" },
            new Location { Id = 5, Name = "Medina", Code = "MED", Description = "Holy city" }
        };

        public LocationManagementWindow()
        {
            InitializeComponent();
            Locations = new ObservableCollection<Location>();
            lstLocations.ItemsSource = Locations;
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                Locations.Clear();
                foreach (var item in _locations.OrderBy(x => x.Name))
                {
                    Locations.Add(item);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading locations: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void lstLocations_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedLocation = lstLocations.SelectedItem as Location;
            
            if (SelectedLocation != null)
            {
                txtName.Text = SelectedLocation.Name;
                txtCode.Text = SelectedLocation.Code;
                txtDescription.Text = SelectedLocation.Description;
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
            if (string.IsNullOrWhiteSpace(txtName.Text) || string.IsNullOrWhiteSpace(txtCode.Text))
            {
                MessageBox.Show("Please enter a name and location code.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var newLocation = new Location
            {
                Name = txtName.Text.Trim(),
                Code = txtCode.Text.Trim().ToUpper(),
                Description = txtDescription.Text.Trim(),
                CreatedDate = DateTime.UtcNow,
                CreatedBy = "CurrentUser"
            };

            try
            {
                newLocation.Id = _locations.Count > 0 ? _locations.Max(x => x.Id) + 1 : 1;
                
                if (_locations.Any(x => x.Name.Equals(newLocation.Name, StringComparison.OrdinalIgnoreCase) || 
                                       x.Code.Equals(newLocation.Code, StringComparison.OrdinalIgnoreCase)))
                {
                    MessageBox.Show("A location with this name or code already exists.", "Duplicate Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                _locations.Add(newLocation);
                Locations.Add(newLocation);
                ClearForm();
                MessageBox.Show("Location added successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding location: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedLocation == null || string.IsNullOrWhiteSpace(txtName.Text) || string.IsNullOrWhiteSpace(txtCode.Text))
            {
                MessageBox.Show("Please select a location and enter a name and location code.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            SelectedLocation.Name = txtName.Text.Trim();
            SelectedLocation.Code = txtCode.Text.Trim().ToUpper();
            SelectedLocation.Description = txtDescription.Text.Trim();
            SelectedLocation.UpdatedDate = DateTime.UtcNow;
            SelectedLocation.UpdatedBy = "CurrentUser";

            try
            {
                if (_locations.Any(x => x.Id != SelectedLocation.Id && 
                                       (x.Name.Equals(SelectedLocation.Name, StringComparison.OrdinalIgnoreCase) || 
                                        x.Code.Equals(SelectedLocation.Code, StringComparison.OrdinalIgnoreCase))))
                {
                    MessageBox.Show("A location with this name or code already exists.", "Duplicate Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                var index = Locations.IndexOf(SelectedLocation);
                if (index >= 0)
                {
                    Locations[index] = SelectedLocation;
                }
                lstLocations.SelectedItem = SelectedLocation;
                MessageBox.Show("Location updated successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating location: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedLocation == null)
            {
                MessageBox.Show("Please select a location to delete.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var result = MessageBox.Show($"Are you sure you want to delete '{SelectedLocation.Name}'?", 
                "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    SelectedLocation.UpdatedDate = DateTime.UtcNow;
                    SelectedLocation.UpdatedBy = "CurrentUser";

                    Locations.Remove(SelectedLocation);
                    ClearForm();
                    MessageBox.Show("Location deleted successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting location: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
            txtCode.Clear();
            txtDescription.Clear();
            SelectedLocation = null;
            lstLocations.SelectedItem = null;
            btnAdd.IsEnabled = true;
            btnUpdate.IsEnabled = false;
            btnDelete.IsEnabled = false;
        }

        public static List<Location> GetLocations()
        {
            return _locations.OrderBy(x => x.Name).ToList();
        }
    }
}
