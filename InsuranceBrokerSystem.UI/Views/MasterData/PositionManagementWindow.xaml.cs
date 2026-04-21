using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using InsuranceBrokerSystem.UI.Interface;
using InsuranceBrokerSystem.UI.Services;
using InsuranceBrokerSystem.Application.DTOs.Master_Table.Position;

namespace InsuranceBrokerSystem.UI.Views.MasterData
{
    public partial class PositionManagementWindow : Window
    {
        private readonly IServiceContainer _service;
        public ObservableCollection<GetPositionDTO> Positions { get; set; }
        public GetPositionDTO SelectedPosition { get; private set; }

        public PositionManagementWindow()
        {
            InitializeComponent();
            _service = new ServiceContainer(new HttpClientService());
            Positions = new ObservableCollection<GetPositionDTO>();
            lstPositions.ItemsSource = Positions;
            LoadDataAsync();
        }

        private async Task LoadDataAsync()
        {
            try
            {
                Positions.Clear();
                var response = await _service.PositionApiService.GetAllPositionsAsync();
                if (response.Successed && response.Data != null)
                {
                    foreach (var item in response.Data.OrderBy(x => x.Level).ThenBy(x => x.Name))
                    {
                        Positions.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading positions: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void lstPositions_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedPosition = lstPositions.SelectedItem as GetPositionDTO;
            if (SelectedPosition != null)
            {
                txtName.Text = SelectedPosition.Name;
                txtCode.Text = SelectedPosition.Code;
                txtDescription.Text = SelectedPosition.Description;
                txtLevel.Text = SelectedPosition.Level.ToString();

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
                MessageBox.Show("Please enter position name.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!int.TryParse(txtLevel.Text, out int level) || level < 1)
            {
                MessageBox.Show("Please enter a valid level (minimum 1).", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                var newPosition = new AddPositionDTO
                {
                    Name = txtName.Text.Trim(),
                    Code = txtCode.Text.Trim(),
                    Description = txtDescription.Text.Trim(),
                    Level = level,
                    IsActive = true
                };

                var response = await _service.PositionApiService.AddPositionAsync(newPosition);
                if (response.Successed)
                {
                    MessageBox.Show("Position added successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    ClearForm();
                    await LoadDataAsync();
                }
                else
                {
                    MessageBox.Show($"Error adding position: {response.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding position: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedPosition == null || string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Please select a position and enter position name.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!int.TryParse(txtLevel.Text, out int level) || level < 1)
            {
                MessageBox.Show("Please enter a valid level (minimum 1).", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                var updatePosition = new UpdatePositionDTO
                {
                    Id = SelectedPosition.Id,
                    Name = txtName.Text.Trim(),
                    Code = txtCode.Text.Trim(),
                    Description = txtDescription.Text.Trim(),
                    Level = level,
                    IsActive = SelectedPosition.IsActive
                };

                var response = await _service.PositionApiService.UpdatePositionAsync(updatePosition);
                if (response.Successed)
                {
                    MessageBox.Show("Position updated successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    ClearForm();
                    await LoadDataAsync();
                }
                else
                {
                    MessageBox.Show($"Error updating position: {response.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating position: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedPosition == null)
            {
                MessageBox.Show("Please select a position to delete.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var result = MessageBox.Show($"Are you sure you want to delete '{SelectedPosition.Name}'?", "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    var response = await _service.PositionApiService.DeletePositionAsync(SelectedPosition.Id);
                    if (response.Successed)
                    {
                        MessageBox.Show("Position deleted successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                        ClearForm();
                        await LoadDataAsync();
                    }
                    else
                    {
                        MessageBox.Show($"Error deleting position: {response.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting position: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
            txtLevel.Text = "1";
            lstPositions.SelectedItem = null;
            SelectedPosition = null;
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
