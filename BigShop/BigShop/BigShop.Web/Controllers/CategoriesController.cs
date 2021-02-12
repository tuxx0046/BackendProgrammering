using BigShop.Models.Category;
using BigShop.Models.Product;
using BigShop.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BigShop.Web.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IProductRepository _productRepository;

        public CategoriesController(ICategoryRepository categoryRepository, IProductRepository productRepository)
        {
            _categoryRepository = categoryRepository;
            _productRepository = productRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<Category>))]
        [ProducesResponseType(400)]
        public async Task<ActionResult<List<Category>>> GetAll()
        {
            var categories = await _categoryRepository.GetAllAsync();
            if (categories == null)
            {
                return BadRequest();
            }
            return Ok(categories);
        }

        [HttpGet("{categoryId}", Name = "GetById")]
        [ProducesResponseType(200, Type = typeof(Category))]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Category>> GetById(int categoryId)
        {
            var category = await _categoryRepository.GetByIdAsync(categoryId);
            if (category == null)
            {
                return NotFound("Category does not exist");
            }
            return Ok(category);
        }

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(Category))]
        [ProducesResponseType(400)]
        public async Task<ActionResult> Create([FromBody] CategoryCreate categoryCreate)
        {
            if (ModelState.IsValid == false)
                return StatusCode(400, ModelState);

            int categoryId = await _categoryRepository.CreateAsync(categoryCreate);
            var newCategory = await _categoryRepository.GetByIdAsync(categoryId);
            
            return CreatedAtRoute("GetById", new { categoryId = newCategory.Id }, newCategory);
        }

        [HttpDelete("{categoryId}", Name = "Delete")]
        [ProducesResponseType(204)] //no content
        [ProducesResponseType(404)]
        public async Task<ActionResult<int>> Delete(int categoryId)
        {
            var category = await _categoryRepository.GetByIdAsync(categoryId);
            if (category == null)
                return NotFound($"Category with Id {categoryId} does not exist");

            int affectedrows = await _categoryRepository.DeleteAsync(categoryId);
            return NoContent();

        }

        [HttpPut("{categoryId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Category>> Update(int categoryId, [FromBody]Category updatedCategory)
        {
            if (categoryId != updatedCategory.Id)
            {
                return BadRequest("Trying to edit category with other Id");
            }

            if (ModelState.IsValid == false)
                return BadRequest(ModelState);

            var oldCategory = await _categoryRepository.GetByIdAsync(categoryId);
            if (oldCategory == null)
            {
                return NotFound($"Category with Id {categoryId} not found");
            }

            await _categoryRepository.UpdateAsync(updatedCategory);

            return Ok();
        }

        [HttpGet("{categoryId}/products")]
        public async Task<ActionResult<List<Product>>> GetProductByCategoryId(int categoryId)
        {
            var categoryExist = await _categoryRepository.GetByIdAsync(categoryId);
            if (categoryExist == null)
            {
                return NotFound($"Category with Id {categoryId} not found");
            }
            
            var products = await _productRepository.GetByCategoryIdAsync(categoryId);
            if (products.Count == 0)
            {
                return Ok($"No products in category \"{categoryExist.Name}\" found");
            }

            return Ok(products);
        }

    }
}
