using Microsoft.AspNetCore.Mvc;
using RealEstateWeb.Data;
using RealEstateWeb.Models;
using System.Linq;

namespace RealEstateWeb.Controllers
{
    public class PropertyController : Controller
    {
        private readonly AppDbContext _context;

        public PropertyController(AppDbContext context)
        {
            _context = context;
        }

        // 🔹SỬA: Index với 3 tham số lọc
        public IActionResult Index(
            string name,            // 🔹THÊM: tìm theo tên
            string location,        // 🔹THÊM: tìm theo địa điểm
            string sort,            // 🔹THÊM: sắp xếp giá
            int page = 1,
            int pageSize = 6)
        {
            var query = _context.Properties.AsQueryable();

            // 🔹SỬA: lọc theo tên
            if (!string.IsNullOrWhiteSpace(name))
            {
                var keyword = name.Trim().ToLower();
                if (keyword.Length >= 3)
                {
                    query = query.Where(p => p.Title.ToLower().Contains(keyword));
                }
            }

            // 🔹SỬA: lọc theo địa điểm
            if (!string.IsNullOrWhiteSpace(location))
            {
                var keyword = location.Trim().ToLower();
                if (keyword.Length >= 3)
                {
                    query = query.Where(p => p.Address.ToLower().Contains(keyword));
                }
            }

            // 🔹SỬA: sort giá
            if (!string.IsNullOrEmpty(sort))
            {
                if (sort == "price_asc")
                {
                    query = query.OrderBy(p => p.Price);
                }
                else if (sort == "price_desc")
                {
                    query = query.OrderByDescending(p => p.Price);
                }
                else
                {
                    query = query.OrderByDescending(p => p.Id);
                }
            }
            else
            {
                query = query.OrderByDescending(p => p.Id);
            }

            // đếm tổng số
            int totalItems = query.Count();

            // lấy dữ liệu phân trang
            var properties = query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            // 🔹THÊM: ViewBag cho từng field lọc
            ViewBag.TotalPages = (int)System.Math.Ceiling(totalItems / (double)pageSize);
            ViewBag.CurrentPage = page;
            ViewBag.Name = name;
            ViewBag.Location = location;
            ViewBag.Sort = sort;

            return View(properties);
        }

        // 🔹SỬA: Sale cũng có 3 tham số lọc
        public IActionResult Sale(
            string name,
            string location,
            string sort,
            int page = 1,
            int pageSize = 6)
        {
            var query = _context.Properties
                .Where(p => p.Status.ToLower() == "for sale");

            if (!string.IsNullOrWhiteSpace(name))
            {
                var keyword = name.Trim().ToLower();
                if (keyword.Length >= 3)
                {
                    query = query.Where(p => p.Title.ToLower().Contains(keyword));
                }
            }

            if (!string.IsNullOrWhiteSpace(location))
            {
                var keyword = location.Trim().ToLower();
                if (keyword.Length >= 3)
                {
                    query = query.Where(p => p.Address.ToLower().Contains(keyword));
                }
            }

            if (!string.IsNullOrEmpty(sort))
            {
                if (sort == "price_asc")
                    query = query.OrderBy(p => p.Price);
                else if (sort == "price_desc")
                    query = query.OrderByDescending(p => p.Price);
                else
                    query = query.OrderByDescending(p => p.Id);
            }
            else
            {
                query = query.OrderByDescending(p => p.Id);
            }

            int totalItems = query.Count();

            var propertiesForSale = query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            ViewBag.TotalPages = (int)System.Math.Ceiling(totalItems / (double)pageSize);
            ViewBag.CurrentPage = page;
            ViewBag.Name = name;
            ViewBag.Location = location;
            ViewBag.Sort = sort;

            return View(propertiesForSale);
        }

        // 🔹SỬA: Rent cũng tương tự
        public IActionResult Rent(
            string name,
            string location,
            string sort,
            int page = 1,
            int pageSize = 6)
        {
            var query = _context.Properties
                .Where(p => p.Status.ToLower() == "for rent");

            if (!string.IsNullOrWhiteSpace(name))
            {
                var keyword = name.Trim().ToLower();
                if (keyword.Length >= 3)
                {
                    query = query.Where(p => p.Title.ToLower().Contains(keyword));
                }
            }

            if (!string.IsNullOrWhiteSpace(location))
            {
                var keyword = location.Trim().ToLower();
                if (keyword.Length >= 3)
                {
                    query = query.Where(p => p.Address.ToLower().Contains(keyword));
                }
            }

            if (!string.IsNullOrEmpty(sort))
            {
                if (sort == "price_asc")
                    query = query.OrderBy(p => p.Price);
                else if (sort == "price_desc")
                    query = query.OrderByDescending(p => p.Price);
                else
                    query = query.OrderByDescending(p => p.Id);
            }
            else
            {
                query = query.OrderByDescending(p => p.Id);
            }

            int totalItems = query.Count();

            var propertiesForRent = query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            ViewBag.TotalPages = (int)System.Math.Ceiling(totalItems / (double)pageSize);
            ViewBag.CurrentPage = page;
            ViewBag.Name = name;
            ViewBag.Location = location;
            ViewBag.Sort = sort;

            return View(propertiesForRent);
        }

        // chi tiết giữ nguyên
        public IActionResult Details(int id)
        {
            var property = _context.Properties.FirstOrDefault(p => p.Id == id);
            if (property == null)
            {
                return NotFound();
            }
            return View(property);
        }
    }
}
