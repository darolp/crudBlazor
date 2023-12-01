using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BlazorCrud.server.Models;
using BlazorCrud.Shared;
using Microsoft.EntityFrameworkCore;

namespace BlazorCrud.server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly DbcrudBlazorContext _dbContext;

        public EmployeeController(DbcrudBlazorContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        [Route("List")]

        public async Task<IActionResult> List()
        {
            var responseApi = new ResponseApi<List<EmployeeDTO>>();
            var listEmployeeDTO = new List<EmployeeDTO>();

            try
            {
                foreach (var item in await _dbContext.Employees.Include(d => d.IdDepartmentNavigation).ToListAsync())
                {
                    listEmployeeDTO.Add(new EmployeeDTO
                    {
                        IdEmployee = item.IdEmployee,
                        FullName = item.FullName,
                        IdDepartment = item.IdDepartment,
                        Salary = item.Salary,
                        DateContract = item.DateContract,
                        Departament = new DepartmentDTO
                        {
                            IdDepartment = item.IdDepartmentNavigation.IdDepartment,
                            DepartmentName = item.IdDepartmentNavigation.DepartmentName
                        }
                    });
                }

                responseApi.Succes = true;
                responseApi.Value = listEmployeeDTO;
            }
            catch (Exception ex)
            {
                responseApi.Succes = false;
                responseApi.Message = ex.Message;
            }
            return Ok(responseApi);
        }

        [HttpGet]
        [Route("Search/{id}")]

        public async Task<IActionResult> Search(int id)
        {
            var responseApi = new ResponseApi<EmployeeDTO>();
            var EmployeeDTO = new EmployeeDTO();

            try
            {
                var dbEmployee = await _dbContext.Employees.FirstOrDefaultAsync(x => x.IdEmployee == id);
                    
                if(dbEmployee != null)
                {
                    EmployeeDTO.IdEmployee = dbEmployee.IdEmployee;
                    EmployeeDTO.FullName = dbEmployee.FullName;
                    EmployeeDTO.IdDepartment = dbEmployee.IdDepartment;
                    EmployeeDTO.Salary = dbEmployee.Salary;
                    EmployeeDTO.DateContract = dbEmployee.DateContract;

                    responseApi.Succes = true;
                    responseApi.Value = EmployeeDTO;
                }
                else
                {
                    responseApi.Succes = false;
                    responseApi.Message = "Not found";
                }

                
            }
            catch (Exception ex)
            {
                responseApi.Succes = false;
                responseApi.Message = ex.Message;
            }
            return Ok(responseApi);
        }

        [HttpPost]
        [Route("AddEmployee")]

        public async Task<IActionResult> AddEmployee(EmployeeDTO Employee)
        {
            var responseApi = new ResponseApi<int>();

            try
            {
                var dbEmployee = new Employee
                {
                    FullName = Employee.FullName,
                    IdDepartment = Employee.IdDepartment,
                    Salary = Employee.Salary,
                    DateContract = Employee.DateContract,
                };
                
                _dbContext.Employees.Add(dbEmployee);
                await _dbContext.SaveChangesAsync();

                if(dbEmployee.IdEmployee != 0)
                {
                    responseApi.Succes = true;
                    responseApi.Value = dbEmployee.IdEmployee;
                }
                else
                {
                    responseApi.Succes = true;
                    responseApi.Message = "Could not be saved";
                }

                
            }
            catch (Exception ex)
            {
                responseApi.Succes = false;
                responseApi.Message = ex.Message;
            }
            return Ok(responseApi);
        }

        [HttpPut]
        [Route("EditEmployee/{id}")]

        public async Task<IActionResult> EditEmployee(EmployeeDTO Employee ,int id)
        {
            var responseApi = new ResponseApi<int>();

            try
            {
                var dbEmployee = await _dbContext.Employees.FirstOrDefaultAsync(e => e.IdEmployee == id);

                

                if (dbEmployee != null)
                {
                    dbEmployee.FullName = Employee.FullName;
                    dbEmployee.Salary = Employee.Salary; 
                    dbEmployee.DateContract = Employee.DateContract;
                    dbEmployee.IdDepartment = Employee.IdDepartment;

                    _dbContext.Employees.Update(dbEmployee);
                    await _dbContext.SaveChangesAsync();

                    responseApi.Succes = true;
                    responseApi.Value = dbEmployee.IdEmployee;
                }
                else
                {
                    responseApi.Succes = true;
                    responseApi.Message = "Employee not found";
                }
                

            }
            catch (Exception ex)
            {
                responseApi.Succes = false;
                responseApi.Message = ex.Message;
            }
            return Ok(responseApi);
        }

        [HttpDelete]
        [Route("DeleteEmployee/{id}")]

        public async Task<IActionResult> DeleteEmployee( int id)
        {
            var responseApi = new ResponseApi<int>();

            try
            {
                var dbEmployee = await _dbContext.Employees.FirstOrDefaultAsync(e => e.IdEmployee == id);



                if (dbEmployee != null)
                {
                    _dbContext.Employees.Remove(dbEmployee);
                    await _dbContext.SaveChangesAsync();

                    responseApi.Succes = true;
                }
                else
                {
                    responseApi.Succes = true;
                    responseApi.Message = "Employee not found";
                }


            }
            catch (Exception ex)
            {
                responseApi.Succes = false;
                responseApi.Message = ex.Message;
            }
            return Ok(responseApi);
        }
    }
}
