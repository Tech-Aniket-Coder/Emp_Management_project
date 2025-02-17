using Emp_Management_project.Models.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emp_Management_project.Models.DTOs
{
    public class SalaryDTO
    {
        public int Id { get; set; }
        [Required]
        public decimal Amount { get; set; }
        public DateTime EffectiveDate { get; set; }
        public int EmployeeId { get; set; }
        public Employee Employee{ get; set; }
    }
}

