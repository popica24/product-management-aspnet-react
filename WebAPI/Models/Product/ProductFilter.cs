using Business.Services;

namespace WebAPI.Models
{
    public class ProductFilter
    {
        public string? Name { get; set; }= string.Empty;
        public int? minQuantity { get; set; } = 0;
        public int? maxQuantity { get; set; } = 0;
        public string orderBy { get; set; } = string.Empty;
        public string groupBy { get; set; } = string.Empty;
        public int CategoryId { get; set; } = 0;
        public ProductSearchParameters ToParameters()
        {
            GroupBy _groupBy = GroupBy.Asc;
            OrderBy _orderBy = OrderBy.Name;

            if (!string.IsNullOrEmpty(groupBy))
            {
                Enum.TryParse(groupBy, out GroupBy _groupByParsed);
                _groupBy = _groupByParsed;

            }
            if (!string.IsNullOrEmpty(orderBy))
            {
                Enum.TryParse(orderBy, out OrderBy _orderByParsed);

                _orderBy = _orderByParsed;

            }

            return new ProductSearchParameters()
            {
                Name = !string.IsNullOrEmpty(Name) ? Name.Trim() : Name,
                categoryId = CategoryId,
                minQuantity = minQuantity,
                maxQuantity = maxQuantity,
                groupBy = _groupBy,
                orderBy = _orderBy,
            };
        }
    }
}
