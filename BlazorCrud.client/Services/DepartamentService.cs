using BlazorCrud.Shared;
using System.Net.Http.Json;

namespace BlazorCrud.client.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly HttpClient _http;

        public DepartmentService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<DepartmentDTO>> List()
        {
            var result = await _http.GetFromJsonAsync<ResponseApi<List<DepartmentDTO>>>("api/Department/List");

            if (result.Succes)
                return result.Value;
            else
                throw new Exception(result.Message);
        }
    }
}
