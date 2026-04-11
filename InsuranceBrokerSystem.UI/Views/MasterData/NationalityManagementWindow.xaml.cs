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
    public partial class NationalityManagementWindow : Window
    {
        private readonly IServiceContainer _service;
        public ObservableCollection<GetNationalityDTO> Nationalities { get; set; }
        public GetNationalityDTO SelectedNationality { get; private set; }

        public NationalityManagementWindow()
        {
            InitializeComponent();
            _service = new ServiceContainer(new HttpClientService());
            Nationalities = new ObservableCollection<GetNationalityDTO>();
            lstNationalities.ItemsSource = Nationalities;
            LoadDataAsync();
        }

        private async Task LoadDataAsync()
        {
            try
            {
                Nationalities.Clear();
                var response = await _service.NationalityApiService.GetAllNationalitiesAsync();
                if (response.Successed && response.Data != null)
                {
                    foreach (var item in response.Data.OrderBy(x => x.NationalityName))
                    {
                        Nationalities.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading nationalities: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void lstNationalities_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedNationality = lstNationalities.SelectedItem as GetNationalityDTO;
            
            if (SelectedNationality != null)
            {
                txtName.Text = SelectedNationality.NationalityName;
                txtCode.Text = SelectedNationality.NationalityName; // Using NationalityName as Code since Code property doesn't exist
                txtDescription.Text = ""; // Description property doesn't exist in GetNationalityDTO
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
                MessageBox.Show("Please enter a name and country code.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // TODO: Implement add functionality using API service
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedNationality == null || string.IsNullOrWhiteSpace(txtName.Text) || string.IsNullOrWhiteSpace(txtCode.Text))
            {
                MessageBox.Show("Please select a nationality and enter a name and country code.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // TODO: Implement update functionality using API service
        }

        // TODO: Implement delete functionality using API service

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
            txtCode.Clear();
            txtDescription.Clear();
            SelectedNationality = null;
            lstNationalities.SelectedItem = null;
            btnAdd.IsEnabled = true;
            btnUpdate.IsEnabled = false;
            btnDelete.IsEnabled = false;
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
