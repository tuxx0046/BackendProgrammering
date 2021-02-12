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

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Category))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Create([FromBody] CategoryCreate categoryCreate)
        {
            if (ModelState.IsValid == false)
            {
                return StatusCode(400, ModelState);
            }

            int newCategoryId = await _categoryRepository.CreateAsync(categoryCreate);
            if (newCategoryId != -1)
            {
                var newCategory = await _categoryRepository.GetByIdAsync(newCategoryId);

                return CreatedAtRoute("GetById", new { categoryId = newCategoryId }, newCategory);
            }

            return StatusCode(500);
        }

        [HttpDelete("{categoryId}", Name = "Delete")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Delete(int categoryId)
        {
            var category = await _categoryRepository.GetByIdAsync(categoryId);
            if (category == null)
            {
                return NotFound($"Category with Id {categoryId} does not exist");
            }

            var products = await _productRepository.GetByCategoryIdAsync(categoryId);
            if(products.Count != 0)
            {
                return BadRequest("Cannot remove category while products are attached to it");
            }

            int affectedRows = await _categoryRepository.DeleteAsync(categoryId);
            if (affectedRows > 0)
            {
                return NoContent();
            }

            return StatusCode(500);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Category>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<Category>>> GetAll()
        {
            var categories = await _categoryRepository.GetAllAsync();
            if (categories.Count == 0)
            {
                return StatusCode(404);
            }

            return Ok(categories);
        }

        [HttpGet("{categoryId}", Name = "GetById")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Category))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Category>> GetById(int categoryId)
        {
            var category = await _categoryRepository.GetByIdAsync(categoryId);
            if (category == null)
            {
                return NotFound("Category does not exist");
            }
            return Ok(category);
        }

        [HttpPut("{categoryId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Category>> Update(int categoryId, [FromBody] Category updatedCategory)
        {
            if (categoryId != updatedCategory.Id)
            {
                return BadRequest("Id does not match");
            }

            var oldCategory = await _categoryRepository.GetByIdAsync(categoryId);
            if (oldCategory == null)
            {
                return NotFound($"Category with Id {categoryId} not found");
            }

            if (ModelState.IsValid == false)
                return BadRequest(ModelState);

            int affectedRows = await _categoryRepository.UpdateAsync(updatedCategory);

            if (affectedRows > 0)
            {
                return Ok();
            }

            return StatusCode(500);
        }

        [HttpGet("{categoryId}/products")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<Product>>> GetProductByCategoryId(int categoryId)
        {
            var category = await _categoryRepository.GetByIdAsync(categoryId);
            if (category == null)
            {
                return NotFound($"Category with Id {categoryId} not found");
            }

            var products = await _productRepository.GetByCategoryIdAsync(categoryId);
            if (products.Count == 0)
            {
                return Ok($"No products in category \"{category.Name}\"");
            }

            return Ok(products);
        }

    }
}
