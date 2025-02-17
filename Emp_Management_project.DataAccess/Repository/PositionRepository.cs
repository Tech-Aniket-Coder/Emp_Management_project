using Emp_Management_project.DataAccess.Data;
using Emp_Management_project.DataAccess.Repository.IRepository;
using Emp_Management_project.Models.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emp_Management_project.DataAccess.Repository
{
    public class PositionRepository : Repository<Position>, IPositionRepository
    {
        private readonly ApplicationDbContext _context;
        public PositionRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public  async Task<IEnumerable<Position>> getAllWithEmployeeAsync()
        {
            return await _context.Positions
                                .Include(d => d.Employees)
                                .ToListAsync();
        }

        public async Task<IEnumerable<Position>> GetPositionsByTeamIdAsync(int teamId)
        {
            return await _context.Positions
                      .Where(t => t.TeamId == teamId)
                      .ToListAsync();
        }
       
        public async Task<int> GetTotalCountAsync(string search)
        {
            var query = _context.Positions.AsQueryable();
            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(d => d.Title.Contains(search));
            }
            return await query.CountAsync();
        }

        public async Task<IEnumerable<Position>> SearchAsync(string search, int page, int pageSize)
        {
            var query = _context.Positions.AsQueryable();
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(d => d.Title.Contains(search));
            }

            query = query.Include(t => t.Team);

            return await query.Skip((page - 1) * pageSize)
                              .Take(pageSize)
                              .ToListAsync();
        }
    }
}
