using Business.Services;

namespace WebAPI.Models
{
    public class ProductFilter
    {
        public string? Name { get; set; }
        public int? Quantity { get; set; }
        public ProductSearchParameters ToParameters(int offset,int limit)
        {
            return new ProductSearchParameters()
            {
                Name = Name,
                Quantity = Quantity,
                Offset = offset,
                Limit = limit   
            };
        }
    }
}
