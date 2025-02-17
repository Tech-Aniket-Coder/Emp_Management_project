using Emp_Management_project.Models.DTOs;
using Emp_Management_project.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emp_Management_project.DataAccess.Services.IServices
{
    public interface IPositionService
    {
        Task<IEnumerable<TeamDTO>> GetAllTeamsAsync();
        Task<IEnumerable<Position>> GetPositionByTeamIdAsync(int teamId);
        Task<(IEnumerable<PositionDTO>, int)> GetAllAsync(int page, int pageSize, string search);
        Task<PositionDTO> GetPositionByIdAsync(int id);
        Task AddPositionAsync(PositionDTO positionDTO);
        Task UpdatePositionAsync(PositionDTO positionDTO);
        Task DeletePositionAsync(int id);
    }
}
