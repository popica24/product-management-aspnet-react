using Business.Domain;
using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models
{
    public class CategoryBody
    {

        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Category description is required !")]
        [MaxLength(255)]
        public string Description { get; set; }

        public int[]? Products { get; set; }
        public Category GetEntity()
        {
            return new Category()
            {
                Name = Name.Trim(),
                Description = Description.Trim(),
                Products = Products != null ? Products.ToList() : new List<int>()
            };
        }
    }
}
