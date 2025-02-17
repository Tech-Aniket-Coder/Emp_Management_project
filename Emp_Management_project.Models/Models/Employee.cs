using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emp_Management_project.Models.Models
{
    public class Employee
    {
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string Gender { get; set; }  
        [Required]
        public string Email { get; set; }
        [Required]
        public string Contact { get; set; }
        public int? TeamId { get; set; }  
        public int? DepartmentId { get; set; }  
        public int? PositionId { get; set; }  

       
        public Team Team { get; set; }
        public Department Department { get; set; }
        public Position Position { get; set; }
        public ICollection<Salary> Salaries { get; set; }

    }
}
