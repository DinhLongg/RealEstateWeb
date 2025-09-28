using Microsoft.AspNetCore.Mvc;

namespace RealEstateWeb.Controllers
{
    public class CartController : Controller
    {
        // Trang giỏ hàng
        public IActionResult Index()
        {
            // sau này sẽ hiển thị các item trong giỏ
            return View();
        }

        // Thêm item vào giỏ
        [HttpPost]
        public IActionResult Add(int propertyId)
        {
            // thêm propertyId vào giỏ (tạm thời)
            TempData["Message"] = $"Đã thêm bất động sản {propertyId} vào giỏ";
            return RedirectToAction("Index");
        }

        // Xoá item khỏi giỏ
        [HttpPost]
        public IActionResult Remove(int propertyId)
        {
            // xoá propertyId khỏi giỏ (tạm thời)
            TempData["Message"] = $"Đã xoá bất động sản {propertyId} khỏi giỏ";
            return RedirectToAction("Index");
        }
    }
}
