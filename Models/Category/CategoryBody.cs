using Business.Domain;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        public Category GetEntity()
        {
            return new Category()
            {
                Name = Name,
                Description = Description
            };
        }
    }
}
