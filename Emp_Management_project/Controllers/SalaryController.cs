using Emp_Management_project.DataAccess.Repository.IRepository;
using Emp_Management_project.DataAccess.Services;
using Emp_Management_project.DataAccess.Services.IServices;
using Emp_Management_project.Models.DTOs;
using Emp_Management_project.Models.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Emp_Management_project.Controllers
{
    public class SalaryController : Controller
    {
        private readonly ISalaryService _salaryService;
        public SalaryController(ISalaryService salaryService)
        {
            _salaryService = salaryService;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> GetAll(string search, int page = 1, int pageSize = 10)
        {
            var (salaries, totalcount) = await _salaryService.GetAllAsync(page, pageSize, search);

            return Json(new
            {
                data = salaries.Select(s => new
                {
                    s.Id,
                    s.Amount,
                    s.EffectiveDate,
                    EmployeeName=s.Employee.FirstName,
                    EmployeeEmail=s.Employee.Email,
                    //EmployeeDepartment=s.Employee.Department.Name,
                    //EmployeeTeam = s.Employee.Team.Name,
                    //EmployeePosition = s.Employee.Position.Title,
                }),
                recordsTotal = totalcount,
                recordsFiltered = totalcount
            });
        }
        public async Task<IActionResult> Upsert(int id, int? employeeId)
        {
            SalaryDTO salaryDTO = new SalaryDTO();

            if (employeeId.HasValue)
            {
                
                salaryDTO.EmployeeId = employeeId.Value;
            }

            if (id != 0)
            {
                salaryDTO = await _salaryService.GetSalaryByIdAsync(id);
                if (salaryDTO == null) return NotFound();
            }

            return View(salaryDTO);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(SalaryDTO salaryDTO)
        {
            if (ModelState.IsValid)
            {
                if (salaryDTO.Id == 0)
                    await _salaryService.AddSalaryAsync(salaryDTO);
                else
                    await _salaryService.UpdateSalaryAsync(salaryDTO);

                return Json(new { success = true, message = "Team saved successfully!" });
            }
            else
                return NotFound();
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var salary = await _salaryService.GetSalaryByIdAsync(id);
            if (salary == null)
            {
                return Json(new { success = false, message = "Salary not found" });
            }
            await _salaryService.DeleteSalaryAsync(id);

            return Json(new { success = true, message = "Salary deleted successfully" });
        }
    }
}
