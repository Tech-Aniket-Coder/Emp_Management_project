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
    public class EmployeeRepository : Repository<Employee>, IEmployeeRepository
    {
        private readonly ApplicationDbContext _context;

        public EmployeeRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Salary>> getAllWithSalaryAsync()
        {
            var salaries = await _context.Salaries
                                          .Include(s => s.Employee) 
                                          .ToListAsync();
            return salaries;
        }

        public async Task<IEnumerable<Employee>> GetEmployeeByDepartmentIdAsync(int departmentId)
        {
            var employees = await _context.Employees
                                          .Where(e => e.DepartmentId == departmentId)
                                          .Include(e => e.Department) 
                                          .Include(e => e.Position) 
                                          .Include(e => e.Team) 
                                          .ToListAsync();
            return employees;
        }

        public async Task<IEnumerable<Employee>> GetEmployeeByPositionIdAsync(int positionId)
        {
            var employees = await _context.Employees
                                          .Where(e => e.PositionId == positionId)
                                          .Include(e => e.Position) 
                                          .Include(e => e.Department) 
                                          .Include(e => e.Team) 
                                          .ToListAsync();
            return employees;
        }
        public async Task<IEnumerable<Employee>> GetEmployeeByTeamIdAsync(int teamId)
        {
            var employees = await _context.Employees
                                          .Where(e => e.TeamId == teamId)
                                          .Include(e => e.Team) 
                                          .Include(e => e.Department) 
                                          .Include(e => e.Position) 
                                          .ToListAsync();
            return employees;
        }


        public async Task<int> GetTotalCountAsync(string search)
        {
            var query = _context.Employees.AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(t => t.FirstName.Contains(search));
            }

            return await query.CountAsync();
        }


        public async Task<IEnumerable<Employee>> SearchAsync(string search, int page, int pageSize)
        {
            var query = _context.Employees.AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(t => t.FirstName.Contains(search));
            }


            query = query.Include(t => t.Department);
            query = query.Include(t => t.Team);
            query = query.Include(t => t.Position);


            return await query.Skip((page - 1) * pageSize)
                              .Take(pageSize)
                              .ToListAsync();
        }
        public async Task<bool> EmailExistsAsync(string email)
        {
            return await _context.Employees
         .AnyAsync(e => e.Email.ToLower() == email.ToLower());
        }
    }
}
