using InsuranceBrokerSystem.UI;

namespace InsuranceBrokerSystem.UI.Services.Master_Table
{
    public class NationalityApiService
    {
        private readonly HttpClientService _httpClientService;

        public NationalityApiService(HttpClientService httpClientService)
        {
            _httpClientService = httpClientService;
        }

        public async Task<ApiResponse<List<GetNationalityDTO>>> GetAllNationalitiesAsync()
        {
            return await _httpClientService.GetAsync<List<GetNationalityDTO>>(ApiRoutes.MasterTable.Nationality.GetAllNationalities);
        }

        public async Task<ApiResponse<GetNationalityDTO>> GetNationalityByIdAsync(int id)
        {
            return await _httpClientService.GetAsync<GetNationalityDTO>($"{ApiRoutes.MasterTable.Nationality.GetNationalityById}?id={id}");
        }

        public async Task<ApiResponse<GetNationalityDTO>> AddNationalityAsync(AddNationalityDTO dto)
        {
            return await _httpClientService.PostAsync<GetNationalityDTO, AddNationalityDTO>(ApiRoutes.MasterTable.Nationality.AddNationality, dto);
        }

        public async Task<ApiResponse<GetNationalityDTO>> UpdateNationalityAsync(UpdateNationalityDTO dto)
        {
            return await _httpClientService.PutAsync<GetNationalityDTO, UpdateNationalityDTO>(ApiRoutes.MasterTable.Nationality.UpdateNationality, dto);
        }

        public async Task<ApiResponse<string>> DeleteNationalityAsync(int id)
        {
            return await _httpClientService.DeleteAsync($"{ApiRoutes.MasterTable.Nationality.DeleteNationality}?id={id}");
        }
    }

    // DTO Classes
    public class GetNationalityDTO
    {
        public int Id { get; set; }
        public string NationalityName { get; set; }
        public string NationalityNameAr { get; set; }
        public bool IsActive { get; set; }
    }

    public class AddNationalityDTO
    {
        public string NationalityName { get; set; }
        public string NationalityNameAr { get; set; }
    }

    public class UpdateNationalityDTO
    {
        public int Id { get; set; }
        public string NationalityName { get; set; }
        public string NationalityNameAr { get; set; }
        public bool IsActive { get; set; }
    }
}
