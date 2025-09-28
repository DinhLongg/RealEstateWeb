using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RealEstateWeb.Models.ViewModels.Admin;
using RealEstateWeb.Models;

namespace RealEstateWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class EmployeeController : Controller
    {
        private readonly UserManager<ApplicationUser> _userMgr;
        private readonly RoleManager<IdentityRole> _roleMgr;

        public EmployeeController(UserManager<ApplicationUser> userMgr, RoleManager<IdentityRole> roleMgr)
        {
            _userMgr = userMgr;
            _roleMgr = roleMgr;
        }

        // LIST employees (users in role Employee)
        public async Task<IActionResult> Index()
        {
            var users = _userMgr.Users.ToList();
            var list = new List<UserRolesViewModel>();
            foreach (var u in users)
            {
                var roles = await _userMgr.GetRolesAsync(u);
                if (roles.Contains("Employee"))
                {
                    list.Add(new UserRolesViewModel { UserId = u.Id, UserName = u.UserName, Email = u.Email, Roles = roles.ToList() });
                }
            }
            return View(list);
        }

        // Create GET
        public IActionResult Create() => View(new EmployeeCreateViewModel());

        // Create POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EmployeeCreateViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var u = new ApplicationUser { UserName = model.UserName.Trim(), Email = model.Email.Trim(), FullName = model.FullName?.Trim(), EmailConfirmed = true };
            var res = await _userMgr.CreateAsync(u, model.Password);
            if (!res.Succeeded)
            {
                foreach (var e in res.Errors) ModelState.AddModelError("", e.Description);
                return View(model);
            }

            // ensure role Employee exists
            if (!await _roleMgr.RoleExistsAsync("Employee")) await _roleMgr.CreateAsync(new IdentityRole("Employee"));
            await _userMgr.AddToRoleAsync(u, "Employee");

            TempData["Success"] = "Đã tạo nhân viên.";
            return RedirectToAction(nameof(Index));
        }

        // Edit GET
        public async Task<IActionResult> Edit(string id)
        {
            if (string.IsNullOrEmpty(id)) return BadRequest();
            var u = await _userMgr.FindByIdAsync(id);
            if (u == null) return NotFound();

            var model = new EmployeeCreateViewModel { UserName = u.UserName, Email = u.Email, FullName = u.FullName };
            ViewBag.UserId = u.Id;
            return View(model);
        }

        // Edit POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, EmployeeCreateViewModel model)
        {
            var u = await _userMgr.FindByIdAsync(id);
            if (u == null) return NotFound();

            u.UserName = model.UserName.Trim();
            u.Email = model.Email.Trim();
            u.FullName = model.FullName?.Trim();

            var res = await _userMgr.UpdateAsync(u);
            if (!res.Succeeded)
            {
                foreach (var e in res.Errors) ModelState.AddModelError("", e.Description);
                ViewBag.UserId = id;
                return View(model);
            }

            TempData["Success"] = "Đã cập nhật nhân viên.";
            return RedirectToAction(nameof(Index));
        }

        // Delete GET
        public async Task<IActionResult> Delete(string id)
        {
            var u = await _userMgr.FindByIdAsync(id);
            if (u == null) return NotFound();
            return View(u);
        }

        // Delete POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var u = await _userMgr.FindByIdAsync(id);
            if (u == null) return NotFound();

            // Prevent deleting your own admin account by accident
            var cur = await _userMgr.GetUserAsync(User);
            if (cur != null && cur.Id == u.Id)
            {
                TempData["Error"] = "Bạn không thể xóa chính mình.";
                return RedirectToAction(nameof(Index));
            }

            var res = await _userMgr.DeleteAsync(u);
            if (!res.Succeeded)
            {
                TempData["Error"] = string.Join("; ", res.Errors.Select(x => x.Description));
                return RedirectToAction(nameof(Index));
            }

            TempData["Success"] = "Đã xóa nhân viên.";
            return RedirectToAction(nameof(Index));
        }

        // Lock / Unlock
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ToggleLock(string id)
        {
            var u = await _userMgr.FindByIdAsync(id);
            if (u == null) return NotFound();

            if (await _userMgr.IsLockedOutAsync(u))
            {
                await _userMgr.SetLockoutEndDateAsync(u, DateTimeOffset.UtcNow);
                await _userMgr.SetLockoutEnabledAsync(u, false);
                TempData["Success"] = "Đã mở khóa tài khoản.";
            }
            else
            {
                await _userMgr.SetLockoutEndDateAsync(u, DateTimeOffset.UtcNow.AddYears(100));
                await _userMgr.SetLockoutEnabledAsync(u, true);
                TempData["Success"] = "Đã khóa tài khoản.";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
