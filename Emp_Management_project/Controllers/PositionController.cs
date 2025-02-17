using Emp_Management_project.DataAccess.Services;
using Emp_Management_project.DataAccess.Services.IServices;
using Emp_Management_project.Models.DTOs;
using Emp_Management_project.Models.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Emp_Management_project.Controllers
{
    public class PositionController : Controller
    {
        private readonly IPositionService _positiionService;
        public PositionController(IPositionService positionService)
        {
            _positiionService = positionService;
        }
        public async Task<IActionResult> Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> GetAll(string search, int page = 1, int pageSize = 10)
        {

            var (position, totalcount) = await _positiionService.GetAllAsync(page, pageSize, search);

            return Json(new
            {
                data = position.Select(t => new
                {
                    t.Id,
                    t.Title,
                    Team = t.Team.Name
                }),
                recordsTotal = totalcount,
                recordsFiltered = totalcount
            });
        }

        public async Task<IActionResult> Upsert(int id)
        {
           
            PositionDTO positionDTO = new PositionDTO();
            if (id != 0)
            {
                positionDTO = await _positiionService.GetPositionByIdAsync(id);
                if(positionDTO == null) return NotFound();
            }

            var teams = await _positiionService.GetAllTeamsAsync();
            ViewBag.Teams = new SelectList(teams, "Id", "Name");
            return View(positionDTO);

        }
         

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(PositionDTO positionDTO)
        {
            if (!ModelState.IsValid) return NotFound();
            if (positionDTO.Id == 0)
                await _positiionService.AddPositionAsync(positionDTO);
            else
                await _positiionService.UpdatePositionAsync(positionDTO);
            return Json(new { success = true, message = "Position saved successfully!" });
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _positiionService.DeletePositionAsync(id);
            return Json(new { success = true, message = "Position deleted successfully" });
        }


    }
}
