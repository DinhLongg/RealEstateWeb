using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RealEstateWeb.Models.ViewModels.Admin;
using RealEstateWeb.Models;


namespace RealEstateWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> _userMgr;
        private readonly RoleManager<IdentityRole> _roleMgr;

        public UserController(UserManager<ApplicationUser> userMgr, RoleManager<IdentityRole> roleMgr)
        {
            _userMgr = userMgr;
            _roleMgr = roleMgr;
        }

        public async Task<IActionResult> Index()
        {
            var users = _userMgr.Users.ToList();
            var vm = new List<UserRolesViewModel>();
            foreach (var u in users)
            {
                var roles = await _userMgr.GetRolesAsync(u);
                vm.Add(new UserRolesViewModel { UserId = u.Id, UserName = u.UserName, Email = u.Email, Roles = roles.ToList() });
            }
            return View(vm);
        }

        // Edit roles GET
        public async Task<IActionResult> EditRoles(string id)
        {
            var u = await _userMgr.FindByIdAsync(id);
            if (u == null) return NotFound();

            var allRoles = _roleMgr.Roles.Select(r => r.Name!).ToList();
            var userRoles = await _userMgr.GetRolesAsync(u);

            var model = new EditUserRolesViewModel
            {
                UserId = u.Id,
                UserName = u.UserName,
                AllRoles = allRoles.Select(r => new RoleItem { RoleName = r, Selected = userRoles.Contains(r) }).ToList()
            };

            return View(model);
        }

        // Edit roles POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditRoles(EditUserRolesViewModel model)
        {
            var u = await _userMgr.FindByIdAsync(model.UserId);
            if (u == null) return NotFound();

            var currentRoles = await _userMgr.GetRolesAsync(u);
            var selected = model.AllRoles.Where(x => x.Selected).Select(x => x.RoleName).ToList();

            // remove not-selected
            var toRemove = currentRoles.Except(selected).ToArray();
            if (toRemove.Any()) await _userMgr.RemoveFromRolesAsync(u, toRemove);

            // add selected not currently assigned
            var toAdd = selected.Except(currentRoles).ToArray();
            if (toAdd.Any()) await _userMgr.AddToRolesAsync(u, toAdd);

            TempData["Success"] = "Cập nhật quyền thành công.";
            return RedirectToAction(nameof(Index));
        }

        // Delete user
        public async Task<IActionResult> Delete(string id)
        {
            var u = await _userMgr.FindByIdAsync(id);
            if (u == null) return NotFound();
            return View(u);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var u = await _userMgr.FindByIdAsync(id);
            if (u == null) return NotFound();

            var cur = await _userMgr.GetUserAsync(User);
            if (cur != null && cur.Id == u.Id)
            {
                TempData["Error"] = "Bạn không thể xóa chính mình.";
                return RedirectToAction(nameof(Index));
            }

            var res = await _userMgr.DeleteAsync(u);
            if (!res.Succeeded)
            {
                TempData["Error"] = string.Join("; ", res.Errors.Select(e => e.Description));
            }
            else
            {
                TempData["Success"] = "Đã xóa user.";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
