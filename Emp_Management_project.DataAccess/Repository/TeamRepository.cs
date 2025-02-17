using Emp_Management_project.DataAccess.Data;
using Emp_Management_project.DataAccess.Repository.IRepository;
using Emp_Management_project.Models.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Emp_Management_project.DataAccess.Repository
{
    public class TeamRepository : Repository<Team>, ITeamRepository
    {
        private readonly ApplicationDbContext _context;

        public TeamRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Team>> getAllWithPositionAsync()
        {
            return await _context.Teams
                                .Include(d => d.Positions)
                                .ToListAsync();
        }

        public async Task<IEnumerable<Team>> GetTeamsByDepartmentIdAsync(int departmentId)
        {
            return await _context.Teams
                                 .Where(t => t.DepartmentId == departmentId)
                                 .ToListAsync();
        }

        public async Task<int> GetTotalCountAsync(string search)
        {
            var query = _context.Teams.AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(t => t.Name.Contains(search));
            }

            return await query.CountAsync();
        }
        public async Task<IEnumerable<Team>> SearchAsync(string search, int page, int pageSize)
        {
            var query = _context.Teams.AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(t => t.Name.Contains(search));
            }


            query = query.Include(t => t.Department);

            return await query.Skip((page - 1) * pageSize)
                              .Take(pageSize)
                              .ToListAsync();
        }

    }
}
