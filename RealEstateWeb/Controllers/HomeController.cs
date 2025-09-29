using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using RealEstateWeb.Data;
using RealEstateWeb.Models;
using System.Diagnostics;

namespace RealEstateWeb.Controllers
{
    public class HomeController : Controller
    {
        // Trang chủ
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context) // DI AppDbContext
        {
            _context = context;
        }

        // GET: /  (Trang chủ)
        public async Task<IActionResult> Index()
        {
            var featured = await _context.Properties
                .OrderByDescending(p => p.Id)
                .Take(6)
                .ToListAsync();

            // Thống kê nhỏ (hiển thị số item Sale / Rent trên hero nếu muốn)
            ViewBag.SaleCount = await _context.Properties.CountAsync(p => p.Status.ToLower() == "for sale");
            ViewBag.RentCount = await _context.Properties.CountAsync(p => p.Status.ToLower() == "for rent");

            return View(featured);
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
