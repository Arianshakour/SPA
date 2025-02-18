using Microsoft.AspNetCore.Mvc;
using SPA.Application.Services.Interfaces;
using SPA.Domain.Dtoes;

namespace SPA.Presentation.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        public async Task<IActionResult> List(string searchValue, int sort, int page = 1, int pageSize = 4, int isDeleted = 0)
        {
            var list = await _userService.GetAll(searchValue, page, pageSize, sort, isDeleted);
            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                if (isDeleted != 0)
                {
                    return PartialView("~/Views/User/_gridDeletedUser.cshtml", list);
                }
                return PartialView("~/Views/User/_gridUser.cshtml", list);
            }
            return View(list);
        }
        public IActionResult Create()
        {
            return PartialView();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateUserDto user)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false });
            }
            await _userService.AddUser(user);
            return RedirectToAction("List");
        }
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var result = await _userService.GetUser(id);
            if (result == null)
            {
                return NotFound();
            }
            return PartialView("~/Views/User/Details.cshtml", result);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var result = await _userService.GetUserForEdit(id);
            if (result == null)
            {
                return NotFound();
            }
            return PartialView("~/Views/User/Edit.cshtml", result);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(EditUserDto userDto)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false });
            }
            await _userService.EditUser(userDto);
            return RedirectToAction("List");
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _userService.GetUserForDelete(id);
            if (result == null)
            {
                return NotFound();
            }
            return PartialView("~/Views/User/Delete.cshtml", result);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(DeleteUserDto userDto)
        {
            await _userService.DeleteUser(userDto);
            return RedirectToAction("List");
        }
        [HttpPost]
        public async Task<IActionResult> Restore(int id)
        {
            await _userService.RestoreUser(id);
            var list = await _userService.GetAll("", 1, 4, 11, 1);
            return PartialView("~/Views/User/_gridDeletedUser.cshtml", list);
        }
        [HttpPost]
        public async Task<IActionResult> DeletePermanent(int id)
        {
            await _userService.DeletePermanent(id);
            var list = await _userService.GetAll("", 1, 4, 11, 1);
            return PartialView("~/Views/User/_gridDeletedUser.cshtml", list);
        }
        public async Task<IActionResult> ExportToExcel(int isDeleted)
        {
            var stream = await _userService.GenerateExcelFile(isDeleted);
            return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Users.xlsx");
        }
    }
}
