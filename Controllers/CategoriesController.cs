using Business.Contracts;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoriesController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        [HttpGet("categories/{offset?}/{limit?}")]
        public List<CategoryModel> Get([FromRoute]int offset = 0, [FromRoute]int limit = 100)
        {
            return new List<CategoryModel>();
        }
    }

   
}
