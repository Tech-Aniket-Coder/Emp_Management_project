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
    public class SalaryRepository : Repository<Salary>, ISalaryRepository
    {
        private readonly ApplicationDbContext _context;
        public SalaryRepository(ApplicationDbContext context):base(context) 
        {
                _context= context;
        }
        public async Task<IEnumerable<Salary>> GetSalaryByEmployeeIdAsync(int employeeId)
        {
           return await _context.Salaries.Where(s=>s.EmployeeId== employeeId).ToListAsync();
        }

        public  async Task<int> GetTotalCountAsync(string search)
        {
            var query = _context.Salaries.AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                if (DateTime.TryParse(search, out DateTime searchDate))
                {
                    query = query.Where(s => s.EffectiveDate.Date == searchDate.Date);
                }
            }
            return await query.CountAsync();
        }

        public  async Task<IEnumerable<Salary>> SearchAsync(string search, int page, int pageSize)
        {
            var query = _context.Salaries.AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                if (DateTime.TryParse(search, out DateTime searchDate))
                {
                    query = query.Where(s => s.EffectiveDate.Date == searchDate.Date);
                }
            }

            query = query.Include(t => t.Employee);
            

            return await query.Skip((page - 1) * pageSize)
                              .Take(pageSize)
                              .ToListAsync();
        }
    }
}

