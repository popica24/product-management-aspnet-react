using Business.Domain;

namespace WebAPI.Models
{
    public class ProductModel
    {
        public int ProductId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public double Quantity { get; set; }

        public ProductModel(Product x)
        {
            ProductId = x.ProductId;
            Name = x.Name;
            Description = x.Description;
            Quantity = x.Quantity;
        }
    }
}
