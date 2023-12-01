using BlazorCrud.Shared;

namespace BlazorCrud.client.Services
{
    public interface IDepartmentService
    {
        Task<List<DepartmentDTO>> List();
    }
}
