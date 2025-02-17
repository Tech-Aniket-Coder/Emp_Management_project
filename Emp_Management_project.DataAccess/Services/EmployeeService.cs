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
    public class EmployeeService : IEmployeeService
    {
        private readonly IMapper _mapper;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly ITeamRepository _teamRepository;
        private readonly IPositionRepository _positionRepository;
        public EmployeeService(IMapper mapper, IEmployeeRepository employeeRepository,
            IDepartmentRepository departmentRepository, ITeamRepository teamRepository,IPositionRepository positionRepository ) 
        { 
            _mapper = mapper;
            _employeeRepository = employeeRepository;
            _departmentRepository = departmentRepository;
            _teamRepository = teamRepository;
            _positionRepository= positionRepository;
        }

        public async Task AddEmployeeAsync(EmployeeDTO employeeDTO)
        {
            try
            {
                if (await _employeeRepository.EmailExistsAsync(employeeDTO.Email))
                {
                    throw new Exception("The email address is already in use.");
                }

                var employee = _mapper.Map<Employee>(employeeDTO);
                await _employeeRepository.AddAsync(employee);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while Add the team.", ex);
            }
        }
        
        


        public async Task DeleteEmployeeAsync(int id)
        {
            try
            {
                await _employeeRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while deleting the team.", ex);
            }
        }

        public async Task<(IEnumerable<EmployeeDTO> employeeDTos, int totalCount, IEnumerable<object> result)> GetAllAsync(int page, int pageSize, string search)
        {

            try
            {
                var employees = await _employeeRepository.SearchAsync(search, page, pageSize);
                var totalCount = await _employeeRepository.GetTotalCountAsync(search);
                var employeeDTos = _mapper.Map<IEnumerable<EmployeeDTO>>(employees);
                var result = employees.Select(e => new
                {
                    e.Id,
                    e.FirstName,
                    e.Email,
                    e.Gender,
                    e.Address,
                    e.Contact,
                    DepartmentName = e.Department.Name,
                    TeamName = e.Team.Name,
                    PositionName = e.Position.Title
                });
                return (employeeDTos, totalCount, result);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while fetching teams.", ex);
            }
        }

        public async Task<IEnumerable<DepartmentDto>> GetAllDepartmentsAsync()
        {
            try
            {
                var departments = await _departmentRepository.getAllWithTeamsAsync();
                return _mapper.Map<IEnumerable<DepartmentDto>>(departments);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while fetching departments.", ex);
            }
        }

        public  async Task<IEnumerable<PositionDTO>> GetAllPositionsAsync()
        {
            try
            {
                var positions = await _positionRepository.getAllWithEmployeeAsync();
                return _mapper.Map<IEnumerable<PositionDTO>>(positions);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while fetching departments.", ex);
            }
        }

        public async Task<IEnumerable<TeamDTO>> GetAllTeamsAsync()
        {
            try
            {
                var teams = await _teamRepository.getAllWithPositionAsync();
                return _mapper.Map<IEnumerable<TeamDTO>>(teams);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while fetching departments.", ex);
            }
        }

        public  async Task<IEnumerable<Employee>> GetEmployeeByDepartmentIdAsync(int departmentId)
        {
            try
            {
                return await _employeeRepository.GetEmployeeByDepartmentIdAsync(departmentId);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving teams by department.", ex);
            }
        }

        public async Task<EmployeeDTO> GetEmployeeByIdAsync(int id)
        {
            try
            {
                var employee = await _employeeRepository.GetByIdAsync(id);
                return _mapper.Map<EmployeeDTO>(employee);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving the team.", ex);
            }
        }

        public async Task<IEnumerable<Employee>> GetEmployeeByPositionIdAsync(int positionId)
        {
            try
            {
                return await _employeeRepository.GetEmployeeByPositionIdAsync(positionId);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving teams by department.", ex);
            }
        }

        public async Task<IEnumerable<Employee>> GetEmployeeByTeamIdAsync(int teamId)
        {
            try
            {
                return await _employeeRepository.GetEmployeeByTeamIdAsync(teamId);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving teams by department.", ex);
            }
        }

        public async Task UpdateEmployeeAsync(EmployeeDTO employeeDTO)
        {

            try
            {
                var employee = _mapper.Map<Employee>(employeeDTO);
                await _employeeRepository.UpdateAsync(employee);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while Update .", ex);
            }
        }
       
    }

}
