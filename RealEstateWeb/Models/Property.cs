using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RealEstateWeb.Models
{
    public class Property
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; } = string.Empty;

        [StringLength(1000)]
        public string? Description { get; set; }

        // đặt kiểu SQL chính xác để tránh warning/precision loss
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        [Required]
        public string Address { get; set; } = string.Empty;

        // Thông tin thêm (không bắt buộc)
        public int Size { get; set; }
        public int Bed { get; set; }
        public int Bath { get; set; }

        // Trạng thái: "For Sale" hoặc "For Rent"
        [Required]
        [StringLength(50)]
        public string Status { get; set; } = "For Sale";

        [StringLength(100)]
        public string? Type { get; set; }

       
        [Required]
        public string ImageUrl { get; set; } = "~/img/placeholder.png";
    }
}
