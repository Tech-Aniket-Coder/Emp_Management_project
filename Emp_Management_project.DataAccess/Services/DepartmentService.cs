using AutoMapper;
using Emp_Management_project.DataAccess.Repository.IRepository;
using Emp_Management_project.DataAccess.Services.IServices;
using Emp_Management_project.Models.DTOs;
using Emp_Management_project.Models.Models;

public class DepartmentService : IDepartmentService
{
    private readonly IDepartmentRepository _departmentRepository;
    private readonly IMapper _mapper;

    public DepartmentService(IDepartmentRepository departmentRepository, IMapper mapper)
    {
        _departmentRepository = departmentRepository;
        _mapper = mapper;
    }

    public async Task AddDepartmentAsync(DepartmentDto departmentDto)
    {
        var department = _mapper.Map<Department>(departmentDto);
        await _departmentRepository.AddAsync(department);
    }

    public async Task DeleteDepartmentAsync(int id)
    {
        await _departmentRepository.DeleteAsync(id);
    }

    public async Task<(IEnumerable<DepartmentDto>, int)> GetAllAsync(int page, int pageSize, string search)
    {
        var departments = await _departmentRepository.SearchAsync(search, page, pageSize);
        var totalCount = await _departmentRepository.GetTotalCountAsync(search);
        var departmentDtos = _mapper.Map<IEnumerable<DepartmentDto>>(departments);
        return (departmentDtos, totalCount);
    }

    public async Task<DepartmentDto> GetDepartmentByIdAsync(int id)
    {
        var department = await _departmentRepository.GetByIdAsync(id);
        return _mapper.Map<DepartmentDto>(department);
    }

    public async Task UpdateDepartmentAsync(DepartmentDto departmentDto)
    {
        var department = _mapper.Map<Department>(departmentDto);
        await _departmentRepository.UpdateAsync(department);
    }
}
