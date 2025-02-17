using Emp_Management_project.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emp_Management_project.DataAccess.Repository.IRepository
{
    public interface ISalaryRepository:IRepository<Salary>
    { 
        Task<IEnumerable<Salary>> SearchAsync(string search, int page, int pageSize);
        Task<int> GetTotalCountAsync(string search);
        Task<IEnumerable<Salary>> GetSalaryByEmployeeIdAsync(int employeeId);
    }
}
