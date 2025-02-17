using Emp_Management_project.Models.DTOs;
using Emp_Management_project.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emp_Management_project.DataAccess.Services.IServices
{
    public interface ISalaryService
    {
        Task<IEnumerable<EmployeeDTO>> GetAllEmployeeAsync();
        Task<IEnumerable<Salary>> GetSalaryByEmployeeIdAsync(int employeeId);
        Task<(IEnumerable<SalaryDTO>, int)> GetAllAsync(int page, int pageSize, string search);
        Task<SalaryDTO> GetSalaryByIdAsync(int id);
        Task AddSalaryAsync(SalaryDTO salaryDTO);
        Task UpdateSalaryAsync(SalaryDTO salaryDTO);
        Task DeleteSalaryAsync(int id);
    }
}
