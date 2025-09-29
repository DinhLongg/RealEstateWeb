// Controllers/ManageController.cs
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RealEstateWeb.Models;
using RealEstateWeb.Models.ViewModels.Manage;

namespace RealEstateWeb.Controllers
{
    [Authorize] // người dùng phải đăng nhập
    public class ManageController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IWebHostEnvironment _env;

        public ManageController(UserManager<ApplicationUser> userManager, IWebHostEnvironment env)
        {
            _userManager = userManager;
            _env = env;
        }

        // GET: /Manage
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return NotFound();

            var vm = new ManageProfileViewModel
            {
                UserId = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                AvatarUrl = user.AvatarUrl
            };
            return View(vm);
        }

        // POST cập nhật thông tin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateProfile(ManageProfileViewModel model)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return NotFound();

            user.FullName = model.FullName;
            // email có thể đổi nhưng cần xác nhận lại, ở đây tạm thời không
            // user.Email = model.Email;

            if (model.AvatarFile != null && model.AvatarFile.Length > 0)
            {
                var uploads = Path.Combine(_env.WebRootPath, "img", "avatars");
                if (!Directory.Exists(uploads)) Directory.CreateDirectory(uploads);
                var ext = Path.GetExtension(model.AvatarFile.FileName);
                var fileName = $"{Guid.NewGuid():N}{ext}";
                var filePath = Path.Combine(uploads, fileName);
                using (var fs = new FileStream(filePath, FileMode.Create))
                {
                    await model.AvatarFile.CopyToAsync(fs);
                }
                user.AvatarUrl = $"/img/avatars/{fileName}";
            }

            var res = await _userManager.UpdateAsync(user);
            if (res.Succeeded)
            {
                TempData["Success"] = "Đã cập nhật hồ sơ";
            }
            else
            {
                TempData["Error"] = string.Join(",", res.Errors.Select(e => e.Description));
            }

            return RedirectToAction(nameof(Index));
        }

        // POST đổi mật khẩu
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ManageProfileViewModel model)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return NotFound();

            if (string.IsNullOrEmpty(model.CurrentPassword) || string.IsNullOrEmpty(model.NewPassword))
            {
                TempData["Error"] = "Vui lòng nhập đầy đủ mật khẩu.";
                return RedirectToAction(nameof(Index));
            }

            var res = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
            if (res.Succeeded)
            {
                TempData["Success"] = "Đã đổi mật khẩu thành công";
            }
            else
            {
                TempData["Error"] = string.Join(",", res.Errors.Select(e => e.Description));
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
