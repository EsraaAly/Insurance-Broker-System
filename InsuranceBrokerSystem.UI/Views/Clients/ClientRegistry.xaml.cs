using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using InsuranceBrokerSystem.Application.DTOs.Client;
using InsuranceBrokerSystem.Domain.Enums.Client;
using InsuranceBrokerSystem.UI.Interface;
using InsuranceBrokerSystem.UI.Services.Clients;

namespace InsuranceBrokerSystem.UI.Views.Clients
{
    /// <summary>
    /// Interaction logic for ClientRegistry.xaml
    /// </summary>
    public partial class ClientRegistry : UserControl
    {
        private readonly IServiceContainer _service;
        public ObservableCollection<GetClientDTO> Clients { get; set; }

        public ClientRegistry()
        {
            InitializeComponent();
            _service = new ServiceContainer(new HttpClientService());
            Clients = new ObservableCollection<GetClientDTO>();
            DataContext = this;
            //LoadClientsAsync();
        }

        private async Task LoadClientsAsync()
        {
            try
            {
                var searchName = txtClientName.Text.ToLower().Trim();
                var searchProducer = CombProducers.Text.ToLower().Trim();
                var searchAccNo = txtAccNo.Text.ToLower().Trim();
                var searchCRNo = txtCRNo.Text.ToLower().Trim();

                int selectedType = 0;
                if (ComboType.SelectedItem is ComboBoxItem item)
                {
                    int.TryParse(item.Tag?.ToString(), out selectedType);
                }

                //if (string.IsNullOrWhiteSpace(searchName) &&
                //    string.IsNullOrWhiteSpace(searchProducer) &&
                //    string.IsNullOrWhiteSpace(searchAccNo) &&
                //    string.IsNullOrWhiteSpace(searchCRNo) &&
                //    selectedType == 0)
                //{

                //    return;
                //}

                var response = await _service.ClientService.GetAllClientsAsync();

                // Get status filter values
                bool showProspect = chProspect.IsChecked == true;
                bool showActive = ChActive.IsChecked == true;
                bool showBlocked = ChBlocked.IsChecked == true;
                bool showRejected = chRejected.IsChecked == true;

                var filteredClients = response.Data.Where(c =>
                    (string.IsNullOrEmpty(searchName) || c.ClientName.ToLower().Contains(searchName)) &&
                    (string.IsNullOrEmpty(searchProducer) || c.Producer?.ToLower().Contains(searchProducer) == true) &&
                    (string.IsNullOrEmpty(searchAccNo) || c.AccountPremium?.ToLower().Contains(searchAccNo) == true) &&
                    (string.IsNullOrEmpty(searchCRNo) || c.CommercialRegistrationNo?.ToLower().Contains(searchCRNo) == true) &&
                    (selectedType == 0 || c.ClientType == selectedType) &&
                    // Status filtering
                    ((showProspect && c.Status == (int)ClientStatus.Prospect) ||
                     (showActive && c.Status == (int)ClientStatus.Active) ||
                     (showBlocked && c.Status == (int)ClientStatus.Blocked) ||
                     (showRejected && c.Status == (int)ClientStatus.Rejected))
                ).ToList();

                Clients.Clear();
                foreach (var client in filteredClients)
                {
                    Clients.Add(client);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error searching clients: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private async Task ComboType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var selectedType = ComboType.SelectedItem as ComboBoxItem;
                if (selectedType == null || selectedType.Tag?.ToString() == "0")
                {
                    await LoadClientsAsync();
                    return;
                }

                var clientType = int.Parse(selectedType.Tag.ToString());
                var response = await _service.ClientService.GetAllClientsAsync();
                var filteredClients = response.Data.Where(c => c.ClientType == clientType).ToList();

                Clients.Clear();
                foreach (var client in filteredClients)
                {
                    Clients.Add(client);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error filtering clients: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnRegisterNewClient_Click(object sender, RoutedEventArgs e)
        {
            RegisterNewClient popup = new RegisterNewClient();
            if (popup.ShowDialog() == true)
            {
                LoadClientsAsync();
            }
        }

        private void btnExportExcel_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Export to Excel functionality not implemented yet.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void btnPrintList_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Print functionality not implemented yet.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            var parentWindow = Window.GetWindow(this);
            parentWindow?.Close();
        }

        private void PrintButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Print functionality not implemented yet.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private async Task EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is GetClientDTO client)
            {
                try
                {
                    var response = await _service.ClientService.GetClientByIdAsync(client.Id);
                    if (response.Data != null)
                    {
                        RegisterNewClient editWindow = new RegisterNewClient(response.Data);
                        if (editWindow.ShowDialog() == true)
                        {
                            await LoadClientsAsync();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error loading client details: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private async Task ApproveButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is GetClientDTO client)
            {
                try
                {
                    if (client.Id <= 0)
                    {
                        MessageBox.Show("Invalid client ID.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    await _service.ClientService.ApproveClientAsync(client.Id);
                    await LoadClientsAsync();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error approving client: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private async Task RejectButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is GetClientDTO client)
            {
                try
                {
                    await _service.ClientService.RejectClientAsync(client.Id); // TODO: Get current user
                    await LoadClientsAsync();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error rejecting client: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private async Task BlockButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is GetClientDTO client)
            {
                try
                {
                    await _service.ClientService.BlockClientAsync(client.Id); // TODO: Get current user
                    await LoadClientsAsync();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error blocking client: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private async void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var searchWindow = new ClientSearchWindow();
                if (searchWindow.ShowDialog() == true && searchWindow.SelectedClient != null)
                {
                    var selectedClient = searchWindow.SelectedClient;
                    txtClientID.Text = selectedClient.Id.ToString();
                    txtClientName.Text = selectedClient.ClientName;                                
                    
                    // Trigger the filter to show the selected client in the main grid
                    await LoadClientsAsync();
                }
            }
            catch (Exception ex)
            {
                ApiResponseHandler.ShowError($"Error opening search window: {ex.Message}");
            }
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            LoadClientsAsync();
        }
    }
}
