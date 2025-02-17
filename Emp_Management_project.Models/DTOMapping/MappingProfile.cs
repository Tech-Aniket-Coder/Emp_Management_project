using AutoMapper;
using Emp_Management_project.Models.DTOs;
using Emp_Management_project.Models.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emp_Management_project.Models.DTOMapping
{
    public  class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Department, DepartmentDto>().ReverseMap();
            CreateMap<Team, TeamDTO>().ReverseMap();
            CreateMap<Position, PositionDTO>().ReverseMap();
            CreateMap<Employee,EmployeeDTO>().ReverseMap(); 
            CreateMap<Salary, SalaryDTO>().ReverseMap();

        }
    }
}
