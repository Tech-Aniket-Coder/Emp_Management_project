using Emp_Management_project.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emp_Management_project.DataAccess.Repository.IRepository
{
    public interface ITeamRepository : IRepository<Team>
    {
        Task<IEnumerable<Team>> getAllWithPositionAsync();
        Task<IEnumerable<Team>> SearchAsync(string search, int page, int pageSize);
        Task<int> GetTotalCountAsync(string search);
        Task<IEnumerable<Team>> GetTeamsByDepartmentIdAsync(int departmentId);
    }
}
