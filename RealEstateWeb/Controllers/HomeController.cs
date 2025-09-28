using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RealEstateWeb.Models;

namespace RealEstateWeb.Controllers
{
    public class HomeController : Controller
    {
        // Trang chủ
        public IActionResult Index()
        {
            return View();
        }

        // Giới thiệu
        public IActionResult About()
        {
            return View();
        }

        // Liên hệ
        public IActionResult Contact()
        {
            return View();
        }
        public IActionResult Blog()
        {
            // sau này có thể load danh sách bài blog từ DB
            return View();
        }

        // Nếu muốn chi tiết bài viết, thêm:
        public IActionResult BlogDetail(int id)
        {
            // load bài blog theo id, hiện tạm id
            ViewBag.Id = id;
            return View();
        }
    }
}
