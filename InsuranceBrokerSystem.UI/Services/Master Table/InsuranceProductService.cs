using Azure;
using InsuranceBrokerSystem.Application.DTOs.Master_Table.InsuranceCompany;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace InsuranceBrokerSystem.UI.Services.Master_Table
{
    class InsuranceProductService
    {
        private readonly HttpClient _httpClient;

        public InsuranceProductService()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:44314")
            };
        }

        public async Task<List<GetInsuranceProductDTO>> GetInsuranceProductByInsuranceIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"{ ApiRoutes.MasterTable.InsuranceCompProduct.GetInsuranceProductByInsuranceIdAsync}/{id}");

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new Exception($"API Error: {response.StatusCode} - {error}");

            }
            return await response.Content.ReadFromJsonAsync<List<GetInsuranceProductDTO>>();

        }
    }
}
