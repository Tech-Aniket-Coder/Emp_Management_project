using Emp_Management_project.DataAccess.Repository;
using Emp_Management_project.Models.DTOs;
using Emp_Management_project.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emp_Management_project.DataAccess.Services.IServices
{
    public interface IEmployeeService
    {

        Task<IEnumerable<DepartmentDto>> GetAllDepartmentsAsync();
        Task<IEnumerable<TeamDTO>> GetAllTeamsAsync();
        Task<IEnumerable<PositionDTO>> GetAllPositionsAsync();

        Task<IEnumerable<Employee>> GetEmployeeByDepartmentIdAsync(int departmentId);
        Task<IEnumerable<Employee>> GetEmployeeByTeamIdAsync(int teamId);
        Task<IEnumerable<Employee>> GetEmployeeByPositionIdAsync(int positionId);
        Task<(IEnumerable<EmployeeDTO> employeeDTos, int totalCount, IEnumerable<object> result)> GetAllAsync(int page, int pageSize, string search);
        Task<EmployeeDTO> GetEmployeeByIdAsync(int id);
        Task AddEmployeeAsync(EmployeeDTO employeeDTO);
        Task UpdateEmployeeAsync(EmployeeDTO employeeDTO);
        Task DeleteEmployeeAsync(int id);
    }
}
