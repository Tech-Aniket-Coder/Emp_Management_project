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
    public class PositionService : IPositionService
    {
        private readonly IPositionRepository _positionRepository;
        private readonly ITeamRepository _teamRepository;
        private readonly IMapper _mapper;
        public PositionService(IPositionRepository positionRepository, IMapper mapper, ITeamRepository teamRepository)
        {
            _positionRepository = positionRepository;
            _mapper = mapper;
            _teamRepository = teamRepository;
        }
        public async Task AddPositionAsync(PositionDTO positionDTO)
        {
            try
            {
                var position = _mapper.Map<Position>(positionDTO);
                await _positionRepository.AddAsync(position);

            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while Adding departments", ex);
            }
        }

        public async Task DeletePositionAsync(int id)
        {
            try
            {
                await _positionRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while deleting the department.", ex);
            }
        }

        public async Task<(IEnumerable<PositionDTO>, int)> GetAllAsync(int page, int pageSize, string search)
        {

            try
            {
                var position = await _positionRepository.SearchAsync(search, page, pageSize);
                var totalCount = await _positionRepository.GetTotalCountAsync(search);
                var positionDtos = _mapper.Map<IEnumerable<PositionDTO>>(position);
                return (positionDtos, totalCount);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while fetching departments", ex);
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

        public async Task<PositionDTO> GetPositionByIdAsync(int id)
        {
            try
            {
                var position = await _positionRepository.GetByIdAsync(id);
                return _mapper.Map<PositionDTO>(position);
            }
            catch (Exception ex)
            {

                throw new Exception("An error occurred while retrieving the department.", ex);
            }
        }

        public async Task<IEnumerable<Position>> GetPositionByTeamIdAsync(int teamId)
        {
            try
            {
                return await _positionRepository.GetPositionsByTeamIdAsync(teamId);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving teams by department.", ex);
            }
        }
        public async Task UpdatePositionAsync(PositionDTO positionDTO)
        {
            try
            {
                var position = _mapper.Map<Position>(positionDTO);
                await _positionRepository.UpdateAsync(position);
            }
            catch (Exception ex)
            {

                throw new Exception("An error occurred while updating the department.", ex);
            }
        }
    }
}
