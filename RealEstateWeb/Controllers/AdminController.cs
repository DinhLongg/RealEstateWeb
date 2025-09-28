using Microsoft.AspNetCore.Mvc;

namespace RealEstateWeb.Controllers
{
    public class AdminController : Controller
    {
        // Trang dashboard quản trị
        public IActionResult Index()
        {
            return View();
        }

        // Quản lý sản phẩm
        public IActionResult ManageProperties()
        {
            // sau này load danh sách property từ DB
            return View();
        }

        // Quản lý người dùng
        public IActionResult ManageUsers()
        {
            // sau này load danh sách user từ DB
            return View();
        }
    }
}
