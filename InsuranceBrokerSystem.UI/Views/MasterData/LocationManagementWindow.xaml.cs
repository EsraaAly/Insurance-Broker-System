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
    public partial class LocationManagementWindow : Window
    {
        private readonly IServiceContainer _service;
        public ObservableCollection<GetLocationDTO> Locations { get; set; }
        public GetLocationDTO SelectedLocation { get; private set; }

        public LocationManagementWindow()
        {
            InitializeComponent();
            _service = new ServiceContainer(new HttpClientService());
            Locations = new ObservableCollection<GetLocationDTO>();
            lstLocations.ItemsSource = Locations;
            _ = LoadDataAsync();
        }

        private async Task LoadDataAsync()
        {
            try
            {
                Locations.Clear();
                var response = await _service.LocationApiService.GetAllLocationsAsync();
                if (response.Successed && response.Data != null)
                {
                    foreach (var item in response.Data.OrderBy(x => x.Name))
                    {
                        Locations.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading locations: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void lstLocations_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedLocation = lstLocations.SelectedItem as GetLocationDTO;
            
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

        private async void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text) || string.IsNullOrWhiteSpace(txtCode.Text))
            {
                MessageBox.Show("Please enter a name and location code.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                var addLocation = new AddLocationDTO
                {
                    Name = txtName.Text.Trim(),
                    Code = txtCode.Text.Trim(),
                    Description = txtDescription.Text.Trim()
                };

                var response = await _service.LocationApiService.AddLocationAsync(addLocation);
                if (response.Successed)
                {
                    MessageBox.Show("Location added successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    ClearForm();
                    await LoadDataAsync();
                }
                else
                {
                    MessageBox.Show(response.Message ?? "Failed to add location", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding location: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedLocation == null || string.IsNullOrWhiteSpace(txtName.Text) || string.IsNullOrWhiteSpace(txtCode.Text))
            {
                MessageBox.Show("Please select a location and enter a name and location code.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                var updateLocation = new UpdateLocationDTO
                {
                    Id = SelectedLocation.Id,
                    Name = txtName.Text.Trim(),
                    Code = txtCode.Text.Trim(),
                    Description = txtDescription.Text.Trim()
                };

                var response = await _service.LocationApiService.UpdateLocationAsync(updateLocation);
                if (response.Successed)
                {
                    MessageBox.Show("Location updated successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    ClearForm();
                    await LoadDataAsync();
                }
                else
                {
                    MessageBox.Show(response.Message ?? "Failed to update location", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating location: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedLocation == null)
            {
                MessageBox.Show("Please select a location to delete.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var result = MessageBox.Show($"Are you sure you want to delete '{SelectedLocation.Name}'?", "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    var response = await _service.LocationApiService.DeleteLocationAsync(SelectedLocation.Id);
                    if (response.Successed)
                    {
                        MessageBox.Show("Location deleted successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                        ClearForm();
                        await LoadDataAsync();
                    }
                    else
                    {
                        MessageBox.Show(response.Message ?? "Failed to delete location", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
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
            LoadDataAsync();
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

    }
}
