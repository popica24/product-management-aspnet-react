using Business.Contracts;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]/")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ICategoryProductRepository _categoryProduct;

        public CategoriesController(ICategoryRepository categoryRepository, ICategoryProductRepository categoryProduct)
        {
            _categoryRepository = categoryRepository;
            _categoryProduct = categoryProduct;
        }

        [HttpGet("{offset?}/{limit?}")]
        public ActionResult<List<CategoryModel>> Get([FromRoute] int offset = 0, [FromRoute] int limit = 100)
        {
            var list = _categoryRepository.GetAll(offset, limit);

            return list.Any() ? list.Select(x => new CategoryModel(x)).ToList() : NotFound();
        }

        [HttpGet("{id?}")]
        public ActionResult<CategoryModel> Get([FromRoute] int id) {
            var item = _categoryRepository.Get(id);
            return item == null ? NotFound() : new CategoryModel(item);
        }

        [HttpPost]
        public ActionResult Post([FromBody] CategoryBody item)
        {
            if (item == null) return BadRequest("Body could not be parsed");
            if (ModelState.IsValid)
            {
                var entity = item.GetEntity();
                var result = _categoryRepository.Post(entity);
                return result ? NoContent() : BadRequest();
            }
            return BadRequest();
        }

        [HttpDelete("/{id?}")]
        public ActionResult Delete([FromRoute] int id) {
            if (id == 0 || Get(id).Result == NotFound()) return BadRequest();
            var isDeletedFromProductCategory = _categoryProduct.DeleteCategory(id);
            var isDeletedFromCategory =_categoryRepository.Delete(id);
            return (isDeletedFromCategory && isDeletedFromProductCategory) ? NoContent() : NotFound();
        }

        [HttpPut("/{id?}")]
        public ActionResult Put([FromBody]CategoryBody item, [FromRoute] int id) 
        { 
            if(id == 0 || item == null) return BadRequest("Id or item null");
            var result = _categoryRepository.Put(item.GetEntity(), id);
            return result ? NoContent() : BadRequest();
        }
    }

   
}
