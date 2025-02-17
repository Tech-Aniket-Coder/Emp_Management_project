using Emp_Management_project.DataAccess.Services.IServices;
using Emp_Management_project.Models.DTOs;
using Emp_Management_project.Models.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

namespace Emp_Management_project.Controllers
{
    public class TeamController : Controller
    {
        private readonly ITeamService _teamService;

        public TeamController(ITeamService teamService)
        {
            _teamService = teamService;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(string search, int page = 1, int pageSize = 10)
        {
            var (teams, totalcount) = await _teamService.GetAllAsync(page, pageSize, search);

            return Json(new
            {
                data = teams.Select(t => new
                {
                    t.Id,
                    t.Name,
                    Department = t.Department.Name
                }),
                recordsTotal = totalcount,
                recordsFiltered = totalcount
            });
        }

        public async Task<IActionResult> Upsert(int id)
        {
            TeamDTO teamDTO = new TeamDTO();
            if (id != 0)
            {
                teamDTO = await _teamService.GetTeamByIdAsync(id);
                if (teamDTO == null) return NotFound();
            }

            var departments = await _teamService.GetAllDepartmentsAsync();
            ViewBag.Departments = new SelectList(departments, "Id", "Name");

            return View(teamDTO);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(TeamDTO teamDTO)
        {
            if (ModelState.IsValid)
            {
                if (teamDTO.Id == 0)
                    await _teamService.AddTeamAsync(teamDTO);
                else
                    await _teamService.UpdateTeamAsync(teamDTO);

                return Json(new { success = true, message = "Team saved successfully!" });
            }
            else
                return NotFound();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _teamService.DeleteTeamAsync(id);
            return Json(new { success = true, message = "Team deleted successfully" });
        }
    }
}
