using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RealEstateWeb.Data;
using RealEstateWeb.Models;

namespace RealEstateWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ProductController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public ProductController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // INDEX: list + pagination
        public async Task<IActionResult> Index(int page = 1, int pageSize = 10, string q = null)
        {
            var query = _context.Properties.AsQueryable();
            if (!string.IsNullOrWhiteSpace(q))
            {
                var k = q.Trim().ToLower();
                query = query.Where(p => p.Title.ToLower().Contains(k) || p.Address.ToLower().Contains(k));
            }

            var total = await query.CountAsync();
            var items = await query.OrderByDescending(p => p.Id)
                                   .Skip((page - 1) * pageSize)
                                   .Take(pageSize)
                                   .ToListAsync();

            ViewBag.Page = page;
            ViewBag.PageSize = pageSize;
            ViewBag.Total = total;
            ViewBag.Query = q;

            return View(items);
        }

        // CREATE GET
        public IActionResult Create()
        {
            return View();
        }

        // CREATE POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Property model, IFormFile? ImageFile)
        {
            if (!ModelState.IsValid) return View(model);

            if (ImageFile != null && ImageFile.Length > 0)
            {
                var uploads = Path.Combine(_env.WebRootPath, "img", "uploads");
                if (!Directory.Exists(uploads)) Directory.CreateDirectory(uploads);
                var ext = Path.GetExtension(ImageFile.FileName);
                var fileName = $"{Guid.NewGuid():N}{ext}";
                var filePath = Path.Combine(uploads, fileName);
                using (var fs = new FileStream(filePath, FileMode.Create))
                {
                    await ImageFile.CopyToAsync(fs);
                }
                model.ImageUrl = $"/img/uploads/{fileName}";
            }
            else
            {
                model.ImageUrl = model.ImageUrl ?? "/img/placeholder.png";
            }

            _context.Properties.Add(model);
            await _context.SaveChangesAsync();
            TempData["Success"] = "Đã thêm sản phẩm thành công.";
            return RedirectToAction(nameof(Index));
        }

        // EDIT GET
        public async Task<IActionResult> Edit(int id)
        {
            var p = await _context.Properties.FindAsync(id);
            if (p == null) return NotFound();
            return View(p);
        }

        // EDIT POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Property model, IFormFile? ImageFile)
        {
            if (id != model.Id) return BadRequest();
            if (!ModelState.IsValid) return View(model);

            var p = await _context.Properties.FindAsync(id);
            if (p == null) return NotFound();

            // update fields
            p.Title = model.Title;
            p.Description = model.Description;
            p.Price = model.Price;
            p.Address = model.Address;
            p.Size = model.Size;
            p.Bed = model.Bed;
            p.Bath = model.Bath;
            p.Status = model.Status;
            p.Type = model.Type;

            if (ImageFile != null && ImageFile.Length > 0)
            {
                var uploads = Path.Combine(_env.WebRootPath, "img", "uploads");
                if (!Directory.Exists(uploads)) Directory.CreateDirectory(uploads);
                var ext = Path.GetExtension(ImageFile.FileName);
                var fileName = $"{Guid.NewGuid():N}{ext}";
                var filePath = Path.Combine(uploads, fileName);
                using (var fs = new FileStream(filePath, FileMode.Create))
                {
                    await ImageFile.CopyToAsync(fs);
                }
                // delete old image if inside uploads
                if (!string.IsNullOrWhiteSpace(p.ImageUrl) && p.ImageUrl.StartsWith("/img/uploads/"))
                {
                    var old = Path.Combine(_env.WebRootPath, p.ImageUrl.TrimStart('/').Replace('/', Path.DirectorySeparatorChar));
                    if (System.IO.File.Exists(old)) System.IO.File.Delete(old);
                }
                p.ImageUrl = $"/img/uploads/{fileName}";
            }

            _context.Properties.Update(p);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Cập nhật sản phẩm thành công.";
            return RedirectToAction(nameof(Index));
        }

        // DETAILS
        public async Task<IActionResult> Details(int id)
        {
            var p = await _context.Properties.FirstOrDefaultAsync(x => x.Id == id);
            if (p == null) return NotFound();
            return View(p);
        }

        // DELETE GET confirm
        public async Task<IActionResult> Delete(int id)
        {
            var p = await _context.Properties.FindAsync(id);
            if (p == null) return NotFound();
            return View(p);
        }

        // DELETE POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var p = await _context.Properties.FindAsync(id);
            if (p == null) return NotFound();

            // delete image file if in uploads
            if (!string.IsNullOrWhiteSpace(p.ImageUrl) && p.ImageUrl.StartsWith("/img/uploads/"))
            {
                var old = Path.Combine(_env.WebRootPath, p.ImageUrl.TrimStart('/').Replace('/', Path.DirectorySeparatorChar));
                if (System.IO.File.Exists(old)) System.IO.File.Delete(old);
            }

            _context.Properties.Remove(p);
            await _context.SaveChangesAsync();
            TempData["Success"] = "Đã xóa sản phẩm.";
            return RedirectToAction(nameof(Index));
        }
    }
}
