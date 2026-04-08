using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using InsuranceBrokerSystem.Application.DTOs.Client;
using InsuranceBrokerSystem.UI.Services.Clients;

namespace InsuranceBrokerSystem.UI.Views.Clients
{
    public partial class ClientSearchWindow : Window
    {
        private readonly ClientService _clientService;
        public ObservableCollection<GetClientDTO> SearchResults { get; set; }
        public GetClientDTO SelectedClient { get; private set; }

        public ClientSearchWindow()
        {
            InitializeComponent();
            _clientService = new ClientService();
            SearchResults = new ObservableCollection<GetClientDTO>();
            DataContext = this;
            //LoadInitialData();
        }

        private async Task LoadInitialData()
        {
            try
            {
                var response = await _clientService.GetAllClientsAsync();
                if (response.Successed)
                {
                    SearchResults.Clear();

                    if (response.Data.Count == 0)
                    {
                        MessageBox.Show("No records found for clients.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        foreach (var client in response.Data.Take(100)) // Limit to 100 for performance
                        {
                            SearchResults.Add(client);
                        }
                    }
                }
                else
                {
                    ApiResponseHandler.ShowError(response.Message, "Load Error");
                }
            }
            catch (Exception ex)
            {
                ApiResponseHandler.ShowError($"Error loading clients: {ex.Message}");
            }
        }

        private async void txtSearchId_TextChanged(object sender, TextChangedEventArgs e)
        {
            await FilterClients();
        }

        private async void txtSearchName_TextChanged(object sender, TextChangedEventArgs e)
        {
            await FilterClients();
        }

        private async void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            await FilterClients();
        }

        private async Task FilterClients()
        {
            try
            {
                var searchId = txtSearchId.Text?.Trim();
                var searchName = txtSearchName.Text?.Trim();

                if (string.IsNullOrWhiteSpace(searchId) && string.IsNullOrWhiteSpace(searchName))
                {
                    await LoadInitialData();
                    return;
                }

                var allClientsResponse = await _clientService.GetAllClientsAsync();
                if (!allClientsResponse.Successed)
                {
                    ApiResponseHandler.ShowError(allClientsResponse.Message, "Search Error");
                    return;
                }

                var filteredClients = allClientsResponse.Data.Where(c =>
                    (string.IsNullOrWhiteSpace(searchId) || c.Id.ToString().Contains(searchId)) &&
                    (string.IsNullOrWhiteSpace(searchName) || c.ClientName.ToLower().Contains(searchName.ToLower()))
                ).ToList();

                SearchResults.Clear();
                foreach (var client in filteredClients)
                {
                    SearchResults.Add(client);
                }
            }
            catch (Exception ex)
            {
                ApiResponseHandler.ShowError($"Error filtering clients: {ex.Message}");
            }
        }

        private void dgSearchResults_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (dgSearchResults.SelectedItem is GetClientDTO client)
            {
                SelectedClient = client;
                DialogResult = true;
                Close();
            }
            else
            {
                ApiResponseHandler.ShowError("Please select a client from the list.", "No Selection");
            }
        }

        private void btnSelect_Click(object sender, RoutedEventArgs e)
        {
            if (dgSearchResults.SelectedItem is GetClientDTO client)
            {
                SelectedClient = client;
                DialogResult = true;
                Close();
            }
            else
            {
                ApiResponseHandler.ShowError("Please select a client from the list.", "No Selection");
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                DialogResult = false;
                Close();
            }
            else if (e.Key == Key.Enter && dgSearchResults.SelectedItem is GetClientDTO)
            {
                btnSelect_Click(this, new RoutedEventArgs());
            }
        }
    }
}
