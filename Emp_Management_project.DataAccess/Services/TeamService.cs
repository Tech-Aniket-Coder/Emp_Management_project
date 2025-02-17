using AutoMapper;
using Emp_Management_project.DataAccess.Repository;
using Emp_Management_project.DataAccess.Repository.IRepository;
using Emp_Management_project.DataAccess.Services.IServices;
using Emp_Management_project.Models.DTOs;
using Emp_Management_project.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Emp_Management_project.DataAccess.Services
{
    public class TeamService : ITeamService
    {
        private readonly ITeamRepository _teamRepository;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IMapper _mapper;

        public TeamService(ITeamRepository teamRepository, IMapper mapper, IDepartmentRepository departmentRepository)
        {
            _teamRepository = teamRepository;
            _mapper = mapper;
            _departmentRepository = departmentRepository;
        }

        public async Task AddTeamAsync(TeamDTO teamDTO)
        {
            try
            {
                var team = _mapper.Map<Team>(teamDTO);
                await _teamRepository.AddAsync(team);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while adding the team.", ex);
            }
        }

        public async Task DeleteTeamAsync(int id)
        {
            try
            {
                await _teamRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while deleting the team.", ex);
            }
        }

        public async Task<(IEnumerable<TeamDTO>, int)> GetAllAsync(int page, int pageSize, string search)
        {
            try
            {
                var teams = await _teamRepository.SearchAsync(search, page, pageSize);
                var totalCount = await _teamRepository.GetTotalCountAsync(search);
                var teamDTOs = _mapper.Map<IEnumerable<TeamDTO>>(teams);
                return (teamDTOs, totalCount);
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


        public async Task<TeamDTO> GetTeamByIdAsync(int id)
        {
            try
            {
                var team = await _teamRepository.GetByIdAsync(id);
                return _mapper.Map<TeamDTO>(team);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving the team.", ex);
            }
        }

        public async Task<IEnumerable<Team>> GetTeamsByDepartmentIdAsync(int departmentId)
        {
            try
            {
                return await _teamRepository.GetTeamsByDepartmentIdAsync(departmentId);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving teams by department.", ex);
            }
        }


        public async Task UpdateTeamAsync(TeamDTO teamDTO)
        {
            try
            {
                var team = _mapper.Map<Team>(teamDTO);
                await _teamRepository.UpdateAsync(team);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while updating the team.", ex);
            }
        }
    }
}
