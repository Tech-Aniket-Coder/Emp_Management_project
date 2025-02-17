using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emp_Management_project.Models.Models
{
    public  class Department
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public ICollection<Team> Teams { get; set; }
    }
}
