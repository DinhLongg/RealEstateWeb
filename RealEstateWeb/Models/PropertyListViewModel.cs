using System.Collections.Generic;

namespace RealEstateWeb.Models
{
    public class PropertyListViewModel
    {
        public IEnumerable<Property> Properties { get; set; } = System.Linq.Enumerable.Empty<Property>();

        // paging
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages { get; set; }

        // filters / sort (giữ lại giá trị để view hiện trạng thái filter)
        public string? Keyword { get; set; }
        public string? Status { get; set; }
        public string? Type { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public int? MinBed { get; set; }
        public int? MaxBed { get; set; }
        public string? SortBy { get; set; }
    }
}
