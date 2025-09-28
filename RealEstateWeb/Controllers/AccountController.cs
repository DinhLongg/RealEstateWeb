using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RealEstateWeb.Models;
using RealEstateWeb.Models.ViewModels;
using System.Text;
using Microsoft.AspNetCore.WebUtilities;

namespace RealEstateWeb.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // GET: /Account/Register
        [HttpGet]
        public IActionResult Register() => View(new RegisterViewModel());

        // POST: /Account/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var user = new ApplicationUser
            {
                UserName = model.Username.Trim(),
                Email = model.Email.Trim(),
                FullName = model.FullName?.Trim()
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                foreach (var e in result.Errors) ModelState.AddModelError(string.Empty, e.Description);
                return View(model);
            }

            // generate email confirmation token
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var encodedToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));

            var callbackUrl = Url.Action(nameof(ConfirmEmail), "Account",
                new { userId = user.Id, token = encodedToken }, Request.Scheme);

            // Option: sign in immediately OR require confirm first (we set RequireConfirmedEmail = true earlier)
            // await _signInManager.SignInAsync(user, isPersistent: false);

            TempData["Message"] = "Đăng ký thành công. Kiểm tra email để xác nhận tài khoản.";
            return RedirectToAction(nameof(Login));
        }

        // GET: /Account/Login
        [HttpGet]
        public async Task<IActionResult> Login(string returnUrl = null)
        {
            // clear external cookie
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);
            ViewData["ReturnUrl"] = returnUrl;
            return View(new LoginViewModel());
        }

        // POST: /Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (!ModelState.IsValid) return View(model);

            var user = await _userManager.FindByNameAsync(model.Username.Trim());
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Sai tên đăng nhập hoặc mật khẩu.");
                return View(model);
            }

            //if (!await _userManager.IsEmailConfirmedAsync(user))
            //{
            //    ModelState.AddModelError(string.Empty, "Bạn cần xác nhận email trước khi đăng nhập.");
            //    return View(model);
            //}

            var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, lockoutOnFailure: true);
            if (result.Succeeded)
            {
                if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                    return Redirect(returnUrl);
                return RedirectToAction("Index", "Home");
            }

            if (result.IsLockedOut)
            {
                ModelState.AddModelError(string.Empty, "Tài khoản bị khóa tạm thời do đăng nhập thất bại nhiều lần.");
                return View(model);
            }

            ModelState.AddModelError(string.Empty, "Sai tên đăng nhập hoặc mật khẩu.");
            return View(model);
        }

        // GET: /Account/ConfirmEmail
        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (userId == null || token == null) return RedirectToAction("Index", "Home");

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return NotFound();

            var decoded = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(token));
            var result = await _userManager.ConfirmEmailAsync(user, decoded);
            if (result.Succeeded)
            {
                return View("ConfirmEmail"); // create view to show success
            }

            return View("Error"); // or custom view
        }

        // GET: /Account/ForgotPassword
        [HttpGet]
        public IActionResult ForgotPassword() => View();

        // POST: /Account/ForgotPassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            if (string.IsNullOrWhiteSpace(email)) { ModelState.AddModelError("", "Nhập email"); return View(); }

            var user = await _userManager.FindByEmailAsync(email.Trim());
            if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
            {
                // Không tiết lộ user không tồn tại
                TempData["Message"] = "Nếu email tồn tại, bạn sẽ nhận được link đặt lại.";
                return RedirectToAction(nameof(ForgotPassword));
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var encoded = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));
            var resetLink = Url.Action(nameof(ResetPassword), "Account", new { userId = user.Id, token = encoded }, Request.Scheme);

            // TODO: gửi resetLink qua email / file tùy bạn

            TempData["Message"] = "Nếu email tồn tại, bạn sẽ nhận được link đặt lại.";
            return RedirectToAction(nameof(ForgotPassword));
        }

        // GET: /Account/ResetPassword
        [HttpGet]
        public IActionResult ResetPassword(string userId, string token)
        {
            var model = new ResetPasswordViewModel { UserId = userId, Token = token };
            return View(model);
        }

        // POST: /Account/ResetPassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            var user = await _userManager.FindByIdAsync(model.UserId);
            if (user == null) { ModelState.AddModelError("", "Không hợp lệ"); return View(model); }

            var decoded = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(model.Token));
            var result = await _userManager.ResetPasswordAsync(user, decoded, model.Password);
            if (result.Succeeded) return View("ResetPasswordConfirmation");

            foreach (var e in result.Errors) ModelState.AddModelError("", e.Description);
            return View(model);
        }

        // POST: /Account/Logout
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        // GET: AccessDenied
        [HttpGet]
        public IActionResult AccessDenied() => View();
    }
}
