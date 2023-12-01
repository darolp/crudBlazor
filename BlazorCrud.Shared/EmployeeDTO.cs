using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace BlazorCrud.Shared
{
    public class EmployeeDTO
    {
        public int IdEmployee { get; set; }

        [Required(ErrorMessage = "The field {0} is requerided")]
        public string FullName { get; set; } = null!;
        [Required]
        [Range(1,int.MaxValue, ErrorMessage ="The field {0} is requerided")]
        public int? IdDepartment { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "The field {0} is requerided")]
        public int Salary { get; set; }

        public DepartmentDTO? Departament { get; set; }
        public DateTime DateContract { get; set; }
    }
}
