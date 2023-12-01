using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using BlazorCrud.server.Models;
using BlazorCrud.Shared;
using Microsoft.EntityFrameworkCore;

namespace BlazorCrud.server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly DbcrudBlazorContext _dbContext;

        public DepartmentController(DbcrudBlazorContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        [Route("List")]
        public async Task<IActionResult> List() 
        {
            var responseApi = new ResponseApi<List<DepartmentDTO>>();
            var listDepartamentDTO = new List<DepartmentDTO>();

            try
            {
                foreach (var item in await _dbContext.Departments.ToListAsync())
                {
                    listDepartamentDTO.Add(new DepartmentDTO
                    {
                        IdDepartment = item.IdDepartment,
                        DepartmentName = item.DepartmentName,
                    });
                }

                responseApi.Succes = true;
                responseApi.Value = listDepartamentDTO;
            }catch(Exception ex)
            {
                responseApi.Succes=false;
                responseApi.Message = ex.Message;
            }
                return Ok(responseApi);
        }

    }
}
