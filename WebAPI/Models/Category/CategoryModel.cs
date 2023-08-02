using Business.Domain;

namespace WebAPI.Models
{
    public class CategoryModel
    {
        public CategoryModel(Category x)
        {
            CategoryId = x.CategoryId;
            Name = x.Name;
            Description = x.Description;
            ProductCount = x.ProductCount;
        }

        public int CategoryId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int ProductCount { get; set; }
    }
}
