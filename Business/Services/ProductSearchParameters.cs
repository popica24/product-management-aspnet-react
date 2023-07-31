
namespace Business.Services
{
    public class ProductSearchParameters
    {
        public string? Name { get; set; }
        public int? minQuantity { get; set; }
        public int? maxQuantity { get; set; }
        public int? categoryId { get; set; }
        public OrderBy? orderBy { get; set; }
        public GroupBy? groupBy { get; set; }
    }
}
