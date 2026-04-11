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
                    foreach (var item in response.Data.OrderBy(x => x.CityName))
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
                txtName.Text = SelectedLocation.CityName;
                txtCode.Text = SelectedLocation.CityName; // Using CityName as Code since Code property doesn't exist
                txtDescription.Text = SelectedLocation.Country; // Using Country as Description
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

            // TODO: Implement add functionality using API service
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedLocation == null || string.IsNullOrWhiteSpace(txtName.Text) || string.IsNullOrWhiteSpace(txtCode.Text))
            {
                MessageBox.Show("Please select a location and enter a name and location code.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

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
