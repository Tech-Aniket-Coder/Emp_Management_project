using AutoMapper;
using Emp_Management_project.DataAccess.Repository;
using Emp_Management_project.DataAccess.Repository.IRepository;
using Emp_Management_project.DataAccess.Services.IServices;
using Emp_Management_project.Models.DTOs;
using Emp_Management_project.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emp_Management_project.DataAccess.Services
{
    public class SalaryService : ISalaryService
    {
        private readonly ISalaryRepository _salaryRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;
        public SalaryService(ISalaryRepository salaryRepository,IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _salaryRepository = salaryRepository;
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }
    
        public  async Task AddSalaryAsync(SalaryDTO salaryDTO)
        {
            try
            {
                var salary = _mapper.Map<Salary>(salaryDTO);
                await _salaryRepository.AddAsync(salary);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while adding the team.", ex);
            }
        }

        public async Task DeleteSalaryAsync(int id)
        {
            try
            {
                await _salaryRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while deleting the team.", ex);
            }
        }

        public async Task<(IEnumerable<SalaryDTO>, int)> GetAllAsync(int page, int pageSize, string search)
        {
            try
            {
                var salaries = await _salaryRepository.SearchAsync(search, page, pageSize);
                var totalCount = await _salaryRepository.GetTotalCountAsync(search);
                var salaryDTOs = _mapper.Map<IEnumerable<SalaryDTO>>(salaries);
                return (salaryDTOs, totalCount);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while fetching teams.", ex);
            }
        }

        public async Task<IEnumerable<EmployeeDTO>> GetAllEmployeeAsync()
        {

            try
            {
                var employees = await _employeeRepository.getAllWithSalaryAsync();
                return _mapper.Map<IEnumerable<EmployeeDTO>>(employees);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while fetching departments.", ex);
            }
        }

        public async Task<IEnumerable<Salary>> GetSalaryByEmployeeIdAsync(int employeeId)
        {
            try
            {
                return await _salaryRepository.GetSalaryByEmployeeIdAsync(employeeId);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving teams by department.", ex);
            }

        }

        public async Task<SalaryDTO> GetSalaryByIdAsync(int id)
        {
            try
            {
                var salary = await _salaryRepository.GetByIdAsync(id);
                return _mapper.Map<SalaryDTO>(salary);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving the team.", ex);
            }
        }

        public  async Task UpdateSalaryAsync(SalaryDTO salaryDTO)
        {
            try
            {
                var salary = _mapper.Map<Salary>(salaryDTO);
                await _salaryRepository.UpdateAsync(salary);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while updating the team.", ex);
            }
        }
    }
}
