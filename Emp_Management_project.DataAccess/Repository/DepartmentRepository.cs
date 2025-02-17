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
    public class DepartmentRepository : Repository<Department>, IDepartmentRepository
    {
        private readonly ApplicationDbContext _context;

        public DepartmentRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Department>> getAllWithTeamsAsync()
        {
            return await _context.Departments
                                 .Include(d => d.Teams)
                                 .ToListAsync();
        }
        public async Task<int> GetTotalCountAsync(string search)
        {
            var query = _context.Departments.AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(t => t.Name.Contains(search));
            }

            return await query.CountAsync();
        }
        public async Task<IEnumerable<Department>> SearchAsync(string search, int page, int pageSize)
        {
            var query = _context.Departments.AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(d => d.Name.Contains(search));
            }

            return await query.Skip((page - 1) * pageSize)
                              .Take(pageSize)
                              .ToListAsync();
        }

    }
}
