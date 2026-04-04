using Azure;
using InsuranceBrokerSystem.Application.DTOs.Master_Table.Insurance_Class_and_Line;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;

namespace InsuranceBrokerSystem.UI.Services.Master_Table
{
    public class InsuranceLOBApiService
    {
        private readonly HttpClient _httpClient;

        public InsuranceLOBApiService()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:44314")
            };
        }

        public async Task<List<GetInsuranceLOBDTO>> GetAllLOBAsync()
        {
            var response = await _httpClient.GetAsync(ApiRoutes.MasterTable.InsuranceLOB.GetAllInsuranceLOBs);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new Exception($"API Error: {response.StatusCode} - {error}");
            }

            return await response.Content.ReadFromJsonAsync<List<GetInsuranceLOBDTO>>();
        }

        public async Task<List<GetInsuranceLOBDTO>> GetLOBByClassIdAsync(int classId)
        {
            var response = await _httpClient.GetAsync($"{ApiRoutes.MasterTable.InsuranceLOB.GetLOBByClassIdAsync}/{classId}");

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new Exception($"API Error: {response.StatusCode} - {error}");
            }

            return await response.Content.ReadFromJsonAsync<List<GetInsuranceLOBDTO>>();
        }

        public async Task<bool> AddLOBAsync(AddInsuranceLOBDTO dto)
        {
            var response = await _httpClient.PostAsJsonAsync(ApiRoutes.MasterTable.InsuranceLOB.AddInsuranceLOB,dto);

            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show("Saved Successfully!");
                return true;

            }
            else
            {
                MessageBox.Show("Error: Could not save data to the server.");
            }
            return false;
        }

        public async Task UpdateLOBAsync(UpdateInsuranceLOBDTO dto)
        {
            var response = await _httpClient.PutAsJsonAsync(ApiRoutes.MasterTable.InsuranceLOB.UpdateInsuranceLOB, dto);

            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show("Update Successfully!");
            }
            else
            {
                MessageBox.Show("Error: Could not Update data to the server.");

            }
        }

        public async Task DeleteLOBAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{ApiRoutes.MasterTable.InsuranceLOB.DeleteInsuranceLOB}/{ id}");

            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show("Update Successfully!");
            }
            else
            {
                MessageBox.Show("Error: Could not Update data to the server.");

            }
        }


    }
}
