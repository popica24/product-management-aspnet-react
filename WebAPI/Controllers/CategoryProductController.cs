using Business.Contracts;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryProductController : ControllerBase
    {
        private readonly ICategoryProductRepository _categoryProduct;

        public CategoryProductController(ICategoryProductRepository categoryProduct)
        {
            _categoryProduct = categoryProduct;
        }

        [HttpPost]
        public ActionResult Post([FromBody] CategoryProductBody item)
        {
            if (item == null) { return BadRequest(); }
            if(ModelState.IsValid)
            {
                var entity = item.GetEntity(item);
                var result = _categoryProduct.Post(entity);
                return result ? NoContent() : BadRequest();
            }
            return BadRequest();
        }

        [HttpDelete]
        public ActionResult Delete([FromBody] CategoryProductBody item)
        {
            if(item == null) return BadRequest("Request is null");
            if(ModelState.IsValid)
            {
                var entity = item.GetEntity(item);
                var response = _categoryProduct.Delete(entity);
                return response ? NoContent() : BadRequest();
            }
            return BadRequest();
        }

    }
}
