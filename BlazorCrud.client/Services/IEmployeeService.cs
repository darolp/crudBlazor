using BlazorCrud.Shared;

namespace BlazorCrud.client.Services
{
    public interface IEmployeeService
    {
        Task<List<EmployeeDTO>> List();

        Task<EmployeeDTO> Search(int id);

        Task<int> AddEmployee (EmployeeDTO employee);

        Task<int> EditEmployee(EmployeeDTO employee, int id);

        Task<bool> DeleteEmployee(int id);
    }
}
