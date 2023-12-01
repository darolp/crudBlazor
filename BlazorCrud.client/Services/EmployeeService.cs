using BlazorCrud.Shared;
using System.Net.Http.Json;

namespace BlazorCrud.client.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly HttpClient _http;
        public EmployeeService(HttpClient http) 
        {
            _http = http;
        }
        public async Task<List<EmployeeDTO>> List()
        {
            var result = await _http.GetFromJsonAsync<ResponseApi<List<EmployeeDTO>>>("api/Employee/List");

            if (result.Succes)
                return result.Value;
            else
                throw new Exception(result.Message);
        }
        public async Task<EmployeeDTO> Search(int id)
        {
            var result = await _http.GetFromJsonAsync<ResponseApi<EmployeeDTO>>($"api/Employee/Search/{id}");

            if (result.Succes)
                return result.Value;
            else
                throw new Exception(result.Message);
        }
        public async Task<int> AddEmployee(EmployeeDTO employee)
        {
            var result = await _http.PostAsJsonAsync("api/Employee/AddEmployee", employee);
            var response = await result.Content.ReadFromJsonAsync<ResponseApi<int>>();

            if (response.Succes)
                return response.Value;
            else
                throw new Exception(response.Message);
        }

        public async Task<int> EditEmployee(EmployeeDTO employee, int id)
        {
            var result = await _http.PutAsJsonAsync($"api/Employee/EditEmployee/{employee.IdEmployee}", employee);
            var response = await result.Content.ReadFromJsonAsync<ResponseApi<int>>();

            if (response.Succes)
                return response.Value;
            else
                throw new Exception(response.Message);
        }

        public async Task<bool> DeleteEmployee(int id)
        {
            var result = await _http.DeleteAsync($"api/Employee/DeleteEmployee/{id}");
            var response = await result.Content.ReadFromJsonAsync<ResponseApi<int>>();

            if (response.Succes)
                return response.Succes;
            else
                throw new Exception(response.Message);
        }

    }
}
