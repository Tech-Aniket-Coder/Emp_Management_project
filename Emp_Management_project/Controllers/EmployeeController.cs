using Emp_Management_project.DataAccess.Services;
using Emp_Management_project.DataAccess.Services.IServices;
using Emp_Management_project.Models.DTOs;
using Emp_Management_project.Models.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

namespace Emp_Management_project.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly IDepartmentService _departmentService;
        private readonly IPositionService _positionService;
        private readonly ITeamService _teamService;
        public EmployeeController(IEmployeeService employeeService, IDepartmentService departmentService, IPositionService positionService, ITeamService teamService)
        {
            _employeeService = employeeService;
            _departmentService = departmentService;
            _positionService = positionService;
            _teamService = teamService;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(string search, int page = 1, int pageSize = 10)
        {
            var (employees, totalcount, result) = await _employeeService.GetAllAsync(page, pageSize, search);
            return Json(new
            {
                data = result,
                recordsTotal = totalcount,
                recordsFiltered = totalcount
            });
        }

        public async Task<IActionResult> Upsert(int id)
        {
            EmployeeDTO employeeDTO = new EmployeeDTO();

            if (id != 0)
            {
                employeeDTO = await _employeeService.GetEmployeeByIdAsync(id);
                if (employeeDTO == null) return NotFound();
            }

 
            var departments = await _employeeService.GetAllDepartmentsAsync();
            var teams = await _employeeService.GetAllTeamsAsync();
            var positions = await _employeeService.GetAllPositionsAsync();

            ViewBag.Departments = new SelectList(departments, "Id", "Name");
            ViewBag.Teams = new SelectList(teams, "Id", "Name");
            ViewBag.Positions = new SelectList(positions, "Id", "Title");

            return View(employeeDTO);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(EmployeeDTO employeeDTO)
        {
            if (ModelState.IsValid)
            {
                if (employeeDTO.Id == 0)
                    await _employeeService.AddEmployeeAsync(employeeDTO);
                else
                    await _employeeService.UpdateEmployeeAsync(employeeDTO);

                return Json(new { success = true, message = "Employee saved successfully!" });
            }

            return NotFound();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _employeeService.DeleteEmployeeAsync(id);
            return Json(new { success = true, message = "Employee deleted successfully" });
        }
        [HttpGet]
        public async Task<IActionResult> GetTeamsByDepartmentId(int departmentId)
        {
            var teams = await _teamService.GetTeamsByDepartmentIdAsync(departmentId);
            return Json(new SelectList(teams, "Id", "Name"));
        }
        [HttpGet]
        public async Task<IActionResult> GetPositionByTeamId(int teamId)
        {
            var positions = await _positionService.GetPositionByTeamIdAsync(teamId);
            return Json(new SelectList(positions, "Id", "Title"));
        }


    }
}
