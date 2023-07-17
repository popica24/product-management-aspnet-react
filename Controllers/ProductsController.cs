﻿using Business.Contracts;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [EnableCors("CorsPolicy")]
    [Route("api/[controller]/")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryProductRepository _categoryProduct;

        public ProductsController(IProductRepository productRepository, ICategoryProductRepository categoryProduct)
        {
            _productRepository = productRepository;
            _categoryProduct = categoryProduct;
        }

        [HttpGet("{offset?}/{limit?}")]
        public ActionResult<IEnumerable<ProductModel>> Get(int offset = 0, int limit = 6)
        {
            var items = _productRepository.GetAll(offset,limit);
            return items.Any() ? items.Select(x=>new ProductModel(x)).ToList() :  NoContent();
        }

        [HttpGet("{id?}")]
        public ActionResult<ProductModel> Get(int id)
        {
            var item = _productRepository.Get(id);
            return item == null ? NotFound() : new ProductModel(item);
        }

        [HttpGet("filter/{offset?}/{limit?}")]
        public ActionResult<IEnumerable<ProductModel>> Filter([FromQuery] ProductFilter model,int offset = 0,int limit = 100)
        //https://stackoverflow.com/questions/56585461/how-can-i-pass-object-param-to-get-method-in-web-api
        {
            if (model == null) return BadRequest("Request cannot be null");
            var entity = model.ToParameters(offset,limit);
           
            var result = _productRepository.Filter(entity);
            return result.Any() ? result.Select(x=>new ProductModel(x)).ToList() : NoContent();
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
        public ActionResult Delete(int id)
        {
            if(id == 0 || Get(id).Result == NotFound())return NotFound();

            var isDeletedFromProductCategory = _categoryProduct.DeleteProduct(id);
            var isDeletedFromProduct = _productRepository.Delete(id);
     
            return isDeletedFromProduct && isDeletedFromProductCategory ? NoContent() : BadRequest();
        }

        [HttpPut("{id?}")]
        public ActionResult Put(int id,  [FromBody]ProductBody model)
        {
            if(id == 0 || Get(id).Result == NotFound()) return BadRequest("Id or item null");
            var result = _productRepository.Update(id, model.GetEntity());
            return result ? NoContent() : BadRequest();
        }
    }

   
}