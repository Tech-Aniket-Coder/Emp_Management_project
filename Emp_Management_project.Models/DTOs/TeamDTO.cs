using Emp_Management_project.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Emp_Management_project.Models.DTOs
{
    public class TeamDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int DepartmentId { get; set; }
        
        public DepartmentDto Department { get; set; }

        public ICollection<Position> Positions { get; set; }
        [JsonIgnore]
        public ICollection<Employee> Employees { get; set; }
    }
}
