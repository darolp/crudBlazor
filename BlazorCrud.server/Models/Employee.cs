using System;
using System.Collections.Generic;

namespace BlazorCrud.server.Models;

public partial class Employee
{
    public int IdEmployee { get; set; }

    public string FullName { get; set; } = null!;

    public int? IdDepartment { get; set; }

    public int Salary { get; set; }

    public DateTime DateContract { get; set; }

    public virtual Department IdDepartmentNavigation { get; set; } = null!;
}
