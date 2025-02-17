using Emp_Management_project.DataAccess.Services.IServices;
using Emp_Management_project.Models;
using Emp_Management_project.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Emp_Management_project.Controllers
{
    public class DepartmentController : Controller
    {

        private readonly IDepartmentService _departmentService;
        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }
        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(string search, int page = 1, int pageSize = 10)
        {

            var (departments, totalCount) = await _departmentService.GetAllAsync(page, pageSize, search);
            return Json(new { data = departments, recordsTotal = totalCount, recordsFiltered = totalCount });
        }

        public async Task<IActionResult> Upsert(int id)
        {
            //Create Case
            DepartmentDto departmentDto = new DepartmentDto();
            if (id == 0) return View(departmentDto);
            //Edit Case 
            departmentDto = await _departmentService.GetDepartmentByIdAsync(id);
            if (departmentDto == null) return NotFound();
            return View(departmentDto);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(DepartmentDto departmentDto)
        {
            if (!ModelState.IsValid) return NotFound();
            if (departmentDto.Id == 0)
                await _departmentService.AddDepartmentAsync(departmentDto);
            else
                await _departmentService.UpdateDepartmentAsync(departmentDto);
            return Json(new { success = true, message = "Department saved successfully!" });
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _departmentService.DeleteDepartmentAsync(id);
            return Json(new { success = true, message = "Department deleted successfully" });
        }




    }

}

