using BigShop.Models.Product;
using BigShop.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BigShop.Web.Controllers
{
    [Route("api/[controller]")] //interferes with individual routes set for actions
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;

        public ProductsController(IProductRepository productRepository, ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }


        //[Route("api/categories/{categoryId}/products")]
        ////[HttpGet("{categoryId}/products")]
        //[HttpGet]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //public async Task<ActionResult<List<Product>>> GetProductsByCategoryId(int categoryId)
        //{
        //    var category = await _categoryRepository.GetByIdAsync(categoryId);
        //    if (category == null)
        //    {
        //        return NotFound($"Category with Id {categoryId} not found");
        //    }

        //    var products = await _productRepository.GetByCategoryIdAsync(categoryId);
        //    if (products.Count == 0)
        //    {
        //        return Ok($"No products in category \"{category.Name}\"");
        //    }

        //    return Ok(products);
        //}
    }
}
