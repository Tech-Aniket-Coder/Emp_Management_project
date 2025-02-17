using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emp_Management_project.Models.Models
{
    public class Position
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Position Title is required.")]
        public string Title { get; set; }
        public int TeamId { get; set; }
        public Team Team { get; set; }
        public ICollection<Employee> Employees { get; set; }
    }
}
