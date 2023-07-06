using Business.Domain;
using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models
{
    public class CategoryProductBody
    {
        [Required]
        public int CategoryId { get; set; }

        [Required]
        public int ProductId { get; set; }

        public CategoryProduct GetEntity(CategoryProductBody item)
        {
            return new CategoryProduct()
            {
                CategoryId = item.CategoryId,
                ProductId = item.ProductId,
            };
        }
    }
}
