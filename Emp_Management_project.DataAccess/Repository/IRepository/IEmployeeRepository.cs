using Emp_Management_project.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emp_Management_project.DataAccess.Repository.IRepository
{
    public interface IEmployeeRepository:IRepository<Employee>
    {
        Task<IEnumerable<Salary>> getAllWithSalaryAsync();
        Task<IEnumerable<Employee>> SearchAsync(string search, int page, int pageSize);
        Task<int> GetTotalCountAsync(string search);
        Task<IEnumerable<Employee>> GetEmployeeByDepartmentIdAsync(int departmentId);
        Task<IEnumerable<Employee>> GetEmployeeByTeamIdAsync(int teamId);
        Task<IEnumerable<Employee>> GetEmployeeByPositionIdAsync(int positionId);
        Task<bool> EmailExistsAsync(string email);

    }
}
