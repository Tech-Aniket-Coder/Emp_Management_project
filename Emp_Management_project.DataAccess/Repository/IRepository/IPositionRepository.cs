using Emp_Management_project.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emp_Management_project.DataAccess.Repository.IRepository
{
    public interface IPositionRepository : IRepository<Position>
    {
        Task<IEnumerable<Position>> getAllWithEmployeeAsync();
        Task<IEnumerable<Position>> GetPositionsByTeamIdAsync(int teamId);
        Task<IEnumerable<Position>> SearchAsync(string search, int page, int pageSize);
        Task<int> GetTotalCountAsync(string search);
    }
}
