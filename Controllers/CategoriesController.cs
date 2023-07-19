using Business.Contracts;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [EnableCors("CorsPolicy")]
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
        public ActionResult<Response<CategoryModel>> Get([FromRoute] int offset = 0, [FromRoute] int limit = 100)
        {
            var response = _categoryRepository.GetAll(offset, limit);
            if(response.HasNextPage || response.IsLastPage)
                return new Response<CategoryModel>(response.TotalRecords,response.Data.Select(x => new CategoryModel(x)).ToList());
            return NoContent();
        }

        [HttpGet("{id?}")]
        public ActionResult<CategoryModel> Get([FromRoute] int id) {
            var item = _categoryRepository.Get(id);
            return item == null ? NotFound() : new CategoryModel(item);
        }

        [HttpGet("filter/{offset?}/{limit?}")]
        public ActionResult<Response<CategoryModel>> Filter([FromQuery] CategoryFilter model, int offset = 0, int limit = 100)
        //https://stackoverflow.com/questions/56585461/how-can-i-pass-object-param-to-get-method-in-web-api
        {
            var parameters = model.ToParameters();
            var response = _categoryRepository.Filter(parameters, offset, limit);
            if (response.HasMoreItems || response.IsLastPage) {
                return new Response<CategoryModel>(response.TotalRecords, response.Data.Select(x => new CategoryModel(x)).ToList());
            }
               
            return NoContent();
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

        [HttpDelete("{id?}")]
        public ActionResult Delete([FromRoute] int id) {
            if (id == 0 || Get(id).Result == NotFound()) return BadRequest();
            var isDeletedFromProductCategory = _categoryProduct.DeleteCategory(id);
            var isDeletedFromCategory =_categoryRepository.Delete(id);
            return (isDeletedFromCategory && isDeletedFromProductCategory) ? NoContent() : NotFound();
        }

        [HttpPut("{id?}")]
        public ActionResult Put([FromBody]CategoryBody item, [FromRoute] int id) 
        { 
            if(id == 0 || item == null) return BadRequest("Id or item null");
            var result = _categoryRepository.Put(item.GetEntity(), id);
            return result ? NoContent() : BadRequest();
        }
    }

   
}
