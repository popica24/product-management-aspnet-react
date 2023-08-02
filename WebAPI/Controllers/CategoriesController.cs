using Business.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;

namespace WebAPI.Controllers
{

    [EnableCors("CorsPolicy")]
    [Route("api/[controller]/")]
    [ApiController]
    [Authorize]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ICategoryProductRepository _categoryProduct;
        public CategoriesController(ICategoryRepository categoryRepository, ICategoryProductRepository categoryProduct)
        {
            _categoryRepository = categoryRepository;
            _categoryProduct = categoryProduct;
        }

        [AllowAnonymous]
        [HttpGet("{offset?}/{limit?}")]
        public ActionResult<Response<CategoryModel>> Get([FromRoute] int offset, [FromRoute] int limit)
        {
            var response = _categoryRepository.GetAll(offset, limit);
            if (response.hasMoreItems || !response.isLastPage)
            {
                return new Response<CategoryModel>(response.Data.Select(x => new CategoryModel(x)).ToList(), response.hasMoreItems, response.Count);
            }
            else if (response.Data.Any())
            {
                return new Response<CategoryModel>(response.Data.Select(x => new CategoryModel(x)).ToList(), hasMore: false, response.Count);
            }
            else
            {
                return NoContent();
            }
        }

        [HttpGet("{id?}")]
        public ActionResult<CategoryModel> Get([FromRoute] int id) {
            var item = _categoryRepository.Get(id);
            return item == null ? NotFound() : new CategoryModel(item);
        }

        [AllowAnonymous]
        [HttpGet("filter/{offset?}/{limit?}")]
        public ActionResult<Response<CategoryModel>> Filter([FromQuery] CategoryFilter model, [FromRoute] int offset = 0, [FromRoute] int limit = 100)
        {
            var parameters = model.ToParameters();
            var response = _categoryRepository.Filter(parameters, offset, limit);
            if (response.hasMoreItems || !response.isLastPage)
            {
                return new Response<CategoryModel>(response.Data.Select(x => new CategoryModel(x)).ToList(), response.hasMoreItems, response.Count);
            }
            else if (response.Data.Any())
            {
                return new Response<CategoryModel>(response.Data.Select(x => new CategoryModel(x)).ToList(), hasMore: false, response.Count);
            }
            else
            {
                return NoContent();
            }
        }

        [AllowAnonymous]
        [HttpGet("{id}/products/{offset?}/{limit?}")]
        public ActionResult<Response<ProductModel>> LoadProducts([FromRoute] int id, [FromRoute] int offset = 0, [FromRoute] int limit = 100) {
            var response = _categoryRepository.LoadProducts(id, offset, limit);
            if (response.hasMoreItems || !response.isLastPage)
            {
                return new Response<ProductModel>(response.Data.Select(x => new ProductModel(x)).ToList(), response.hasMoreItems, response.Count);
            }
            else if (response.Data.Any())
            {
                return new Response<ProductModel>(response.Data.Select(x => new ProductModel(x)).ToList(), hasMore: false, response.Count);
            }
            else
            {
                return NoContent();
            }
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
            var isDeletedFromCategory = _categoryRepository.Delete(id);
            return (isDeletedFromCategory && isDeletedFromProductCategory) ? NoContent() : NotFound();
        }

        [HttpPut("{id}")]
        public ActionResult Put([FromBody] CategoryBody item, [FromRoute] int id)
        {
            if (id == 0 || item == null) return BadRequest("Id or item null");
            var result = _categoryRepository.Put(item.GetEntity(), id);
            return result ? NoContent() : BadRequest();
        }

        [HttpDelete("{id?}/{productId?}")]
        public ActionResult DeleteProduct([FromRoute] int id, [FromRoute] int productId)
        {
            if (id == 0 || productId == 0) return BadRequest();
            var result = _categoryProduct.DeleteProduct(productId);
            return result ? NoContent() : BadRequest();
        }

       
    }

   
}
