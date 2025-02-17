using Emp_Management_project.Models.DTOs;
using Emp_Management_project.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emp_Management_project.DataAccess.Services.IServices
{
    public interface ITeamService
    {
        Task<IEnumerable<DepartmentDto>> GetAllDepartmentsAsync();
        Task<IEnumerable<Team>> GetTeamsByDepartmentIdAsync(int departmentId);
        Task<(IEnumerable<TeamDTO>, int)> GetAllAsync(int page, int pageSize, string search);
        Task<TeamDTO> GetTeamByIdAsync(int id);
        Task AddTeamAsync(TeamDTO teamDTO);
        Task UpdateTeamAsync(TeamDTO teamDTO);
        Task DeleteTeamAsync(int id);
    }
}
