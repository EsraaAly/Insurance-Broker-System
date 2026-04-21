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
                    foreach (var item in response.Data.OrderBy(x => x.Name))
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
                txtName.Text = SelectedNationality.Name;
                txtCode.Text = SelectedNationality.Code;
                txtDescription.Text = SelectedNationality.Description;
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
                MessageBox.Show("Please enter a name and country code.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                var addNationality = new AddNationalityDTO
                {
                    Name = txtName.Text.Trim(),
                    Code = txtCode.Text.Trim(),
                    Description = txtDescription.Text.Trim()
                };

                var response = await _service.NationalityApiService.AddNationalityAsync(addNationality);
                if (response.Successed)
                {
                    MessageBox.Show("Nationality added successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    ClearForm();
                    await LoadDataAsync();
                }
                else
                {
                    MessageBox.Show(response.Message ?? "Failed to add nationality", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding nationality: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedNationality == null || string.IsNullOrWhiteSpace(txtName.Text) || string.IsNullOrWhiteSpace(txtCode.Text))
            {
                MessageBox.Show("Please select a nationality and enter a name and country code.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                var updateNationality = new UpdateNationalityDTO
                {
                    Id = SelectedNationality.Id,
                    Name = txtName.Text.Trim(),
                    Code = txtCode.Text.Trim(),
                    Description = txtDescription.Text.Trim()
                };

                var response = await _service.NationalityApiService.UpdateNationalityAsync(updateNationality);
                if (response.Successed)
                {
                    MessageBox.Show("Nationality updated successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    ClearForm();
                    await LoadDataAsync();
                }
                else
                {
                    MessageBox.Show(response.Message ?? "Failed to update nationality", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating nationality: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedNationality == null)
            {
                MessageBox.Show("Please select a nationality to delete.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var result = MessageBox.Show($"Are you sure you want to delete '{SelectedNationality.Name}'?", "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    var response = await _service.NationalityApiService.DeleteNationalityAsync(SelectedNationality.Id);
                    if (response.Successed)
                    {
                        MessageBox.Show("Nationality deleted successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                        ClearForm();
                        await LoadDataAsync();
                    }
                    else
                    {
                        MessageBox.Show(response.Message ?? "Failed to delete nationality", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting nationality: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
            txtCode.Clear();
            txtDescription.Clear();
            SelectedNationality = null;
            lstNationalities.SelectedItem = null;
            btnAdd.IsEnabled = true;
            btnUpdate.IsEnabled = false;
            btnDelete.IsEnabled = false;
        }

    }
}
