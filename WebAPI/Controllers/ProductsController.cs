using Business.Contracts;
using Data.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [EnableCors("CorsPolicy")]
    [Route("api/[controller]/")]
    [ApiController]
    [Authorize]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryProductRepository _categoryProduct;

        public ProductsController(IProductRepository productRepository, ICategoryProductRepository categoryProduct)
        {
            _productRepository = productRepository;
            _categoryProduct = categoryProduct;
        }

        [AllowAnonymous]
        [HttpGet("{offset?}/{limit?}")]
        public ActionResult<Response<ProductModel>> Get([FromRoute] int offset, [FromRoute] int limit)
        {
            var response = _productRepository.GetAll(offset, limit);

            if (response.hasMoreItems || !response.isLastPage)
            {
                return new Response<ProductModel>(response.Data.Select(x => new ProductModel(x)).ToList(), response.hasMoreItems, response.Count);
            }
            else if (response.Data.Any())
            {
                return new Response<ProductModel>(response.Data.Select(x => new ProductModel(x)).ToList(),hasMore: false, response.Count);
            }
            else
            {
                return NoContent();
            }
        }


        [HttpGet("{id?}")]
        public ActionResult<ProductModel> Get([FromRoute] int id)
        {
            var item = _productRepository.Get(id);
            return item == null ? NotFound() : new ProductModel(item);
        }

        [AllowAnonymous]
        [HttpGet("filter/{offset?}/{limit?}")]
        public ActionResult<Response<ProductModel>> Filter([FromQuery] ProductFilter model,int offset = 0,int limit = 100)
        {
            var parameters = model.ToParameters();
            var response = _productRepository.Filter(parameters, offset, limit);
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

        [HttpGet("{id}/categories")]
        public List<string> LoadCategories([FromRoute] int id)
        {
            var data = _productRepository.LoadCategories(id);
            return data;
        }

        [HttpPost]  
        public ActionResult Post([FromBody] ProductBody model)
        {
            if (model == null) return BadRequest("Body could not be parsed");
            if(ModelState.IsValid)
            {
                var entity = model.GetEntity();
                var result = _productRepository.Post(entity);
                return result ? NoContent() : BadRequest();
            }
            return BadRequest();
        }

        [HttpDelete("{id?}")]
        public ActionResult Delete([FromRoute] int id)
        {
            if(id == 0 || Get(id).Result == NotFound())return NotFound();

            var isDeletedFromProductCategory = _categoryProduct.DeleteProduct(id);
            var isDeletedFromProduct = _productRepository.Delete(id);
     
            return isDeletedFromProduct && isDeletedFromProductCategory ? NoContent() : BadRequest();
        }

        [HttpPut("{id?}")]
        public ActionResult Put([FromRoute] int id,  [FromBody]ProductBody model)
        {
            if(id == 0 || Get(id).Result == NotFound()) return BadRequest("Id or item null");
            var result = _productRepository.Put(model.GetEntity(),id);
            return result ? NoContent() : BadRequest();
        }
    }

   
}
