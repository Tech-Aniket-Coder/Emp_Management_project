using Emp_Management_project.Models;
using Emp_Management_project.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emp_Management_project.DataAccess.Services.IServices
{
    public interface IDepartmentService
    {
        Task<(IEnumerable<DepartmentDto>, int)> GetAllAsync(int page, int pageSize, string search);
        Task<DepartmentDto> GetDepartmentByIdAsync(int id);
        Task AddDepartmentAsync(DepartmentDto departmentDto);
        Task UpdateDepartmentAsync(DepartmentDto departmentDto);
        Task DeleteDepartmentAsync(int id);

    }
}
