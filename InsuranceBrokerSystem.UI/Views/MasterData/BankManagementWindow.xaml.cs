using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using InsuranceBrokerSystem.UI.Interface;
using InsuranceBrokerSystem.UI.Services;
using InsuranceBrokerSystem.Application.DTOs.Master_Table.Bank;

namespace InsuranceBrokerSystem.UI.Views.MasterData
{
    public partial class BankManagementWindow : Window
    {
        private readonly IServiceContainer _service;
        public ObservableCollection<GetBankDTO> Banks { get; set; }
        public GetBankDTO SelectedBank { get; private set; }

        public BankManagementWindow()
        {
            InitializeComponent();
            _service = new ServiceContainer(new HttpClientService());
            Banks = new ObservableCollection<GetBankDTO>();
            lstBanks.ItemsSource = Banks;
            LoadDataAsync();
        }

        private async Task LoadDataAsync()
        {
            try
            {
                Banks.Clear();
                var response = await _service.BankApiService.GetAllBanksAsync();
                if (response.Successed && response.Data != null)
                {
                    foreach (var item in response.Data.OrderBy(x => x.Name))
                    {
                        Banks.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading banks: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void lstBanks_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedBank = lstBanks.SelectedItem as GetBankDTO;
            if (SelectedBank != null)
            {
                txtName.Text = SelectedBank.Name;
                txtCode.Text = SelectedBank.Code;
                txtDescription.Text = SelectedBank.Description;
                txtSwiftCode.Text = SelectedBank.SwiftCode;
                chkIsActive.IsChecked = SelectedBank.IsActive;

                btnUpdate.IsEnabled = true;
                btnDelete.IsEnabled = true;
                btnAdd.IsEnabled = false;
            }
            else
            {
                ClearForm();
                btnUpdate.IsEnabled = false;
                btnDelete.IsEnabled = false;
                btnAdd.IsEnabled = true;
            }
        }

        private async void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Please enter bank name.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                var newBank = new AddBankDTO
                {
                    Name = txtName.Text.Trim(),
                    Code = txtCode.Text.Trim(),
                    Description = txtDescription.Text.Trim(),
                    SwiftCode = txtSwiftCode.Text.Trim(),
                    IsActive = chkIsActive.IsChecked ?? true
                };

                var response = await _service.BankApiService.AddBankAsync(newBank);
                if (response.Successed)
                {
                    MessageBox.Show("Bank added successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    ClearForm();
                    await LoadDataAsync();
                }
                else
                {
                    MessageBox.Show($"Error adding bank: {response.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding bank: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedBank == null || string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Please select a bank and enter bank name.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                var updateBank = new UpdateBankDTO
                {
                    Id = SelectedBank.Id,
                    Name = txtName.Text.Trim(),
                    Code = txtCode.Text.Trim(),
                    Description = txtDescription.Text.Trim(),
                    SwiftCode = txtSwiftCode.Text.Trim(),
                    IsActive = chkIsActive.IsChecked ?? true
                };

                var response = await _service.BankApiService.UpdateBankAsync(updateBank);
                if (response.Successed)
                {
                    MessageBox.Show("Bank updated successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    ClearForm();
                    await LoadDataAsync();
                }
                else
                {
                    MessageBox.Show($"Error updating bank: {response.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating bank: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedBank == null)
            {
                MessageBox.Show("Please select a bank to delete.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var result = MessageBox.Show($"Are you sure you want to delete '{SelectedBank.Name}'?", "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    var response = await _service.BankApiService.DeleteBankAsync(SelectedBank.Id);
                    if (response.Successed)
                    {
                        MessageBox.Show("Bank deleted successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                        ClearForm();
                        await LoadDataAsync();
                    }
                    else
                    {
                        MessageBox.Show($"Error deleting bank: {response.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting bank: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            ClearForm();
        }

        private void ClearForm()
        {
            txtName.Clear();
            txtCode.Clear();
            txtDescription.Clear();
            txtSwiftCode.Clear();
            chkIsActive.IsChecked = true;
            lstBanks.SelectedItem = null;
            SelectedBank = null;
            btnAdd.IsEnabled = true;
            btnUpdate.IsEnabled = false;
            btnDelete.IsEnabled = false;
        }

        private async void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            await LoadDataAsync();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
