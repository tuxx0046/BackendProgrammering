using BigShop.Models.Manufacturer;
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
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ManufacturersController : ControllerBase
    {
        private readonly IManufacturerRepository _manufacturerRepository;
        private readonly IProductRepository _productRepository;

        public ManufacturersController(IManufacturerRepository manufacturerRepository,
                                       IProductRepository productRepository)
        {
            _manufacturerRepository = manufacturerRepository;
            _productRepository = productRepository;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Manufacturer))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> CreateManufacturer([FromBody] ManufacturerCreate manufacturerCreate)
        {
            if (ModelState.IsValid == false)
            {
                return StatusCode(400, ModelState);
            }

            int newManufacturerId = await _manufacturerRepository.CreateAsync(manufacturerCreate);
            if (newManufacturerId != -1)
            {
                var newManufacturer = await _manufacturerRepository.GetByIdAsync(newManufacturerId);

                return CreatedAtRoute("GetByManufacturerId", new { manufacturerId = newManufacturerId }, newManufacturer);
            }

            return StatusCode(500);
        }

        [HttpDelete("{manufacturerId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteManufacturer(int manufacturerId)
        {
            var manufacturer = await _manufacturerRepository.GetByIdAsync(manufacturerId);
            if (manufacturer == null)
            {
                return NotFound($"Manufacturer with Id {manufacturerId} does not exist");
            }

            var products = await _productRepository.GetByManufacturerIdAsync(manufacturerId);
            if (products.Count != 0)
            {
                return BadRequest("Cannot remove manufacturers with products attached to it");
            }

            int affectedRows = await _manufacturerRepository.DeleteAsync(manufacturerId);
            if (affectedRows == 1)
            {
                return NoContent();
            }

            return StatusCode(500);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<Manufacturer>>> GetAllManufacturers()
        {
            var manufacturers = await _manufacturerRepository.GetAllAsync();
            if (manufacturers == null)
            {
                return StatusCode(500);
            }

            if (manufacturers.Count == 0)
            {
                return Ok("No manufacturers in database");
            }

            return Ok(manufacturers);
        }

        [HttpGet("{manufacturerId}", Name = "GetByManufacturerId")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Manufacturer>> GetByManufacturerId(int manufacturerId)
        {
            var manufacturer = await _manufacturerRepository.GetByIdAsync(manufacturerId);
            if (manufacturer == null)
            {
                return NotFound($"Manufacturer with Id {manufacturerId} does not exist");
            }
            return Ok(manufacturer);
        }

        [HttpPut("{manufacturerId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> UpdateManufacturer(int manufacturerId, [FromBody] Manufacturer updatedManufacturer)
        {
            if (manufacturerId != updatedManufacturer.Id)
            {
                return BadRequest("Id does not match");
            }

            var oldManufacturer = await _manufacturerRepository.GetByIdAsync(manufacturerId);
            if (oldManufacturer == null)
            {
                return NotFound($"Manufacturer with Id {manufacturerId} not found");
            }

            if (ModelState.IsValid == false)
                return BadRequest(ModelState);

            int affectedRows = await _manufacturerRepository.UpdateAsync(updatedManufacturer);

            if (affectedRows == 1)
            {
                return Ok();
            }

            return StatusCode(500);
        }

        [HttpGet("{manufacturerId}/products")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<Product>>> GetProductsByManufacturerId(int manufacturerId)
        {
            var manufacturer = await _manufacturerRepository.GetByIdAsync(manufacturerId);
            if (manufacturer == null)
            {
                return NotFound($"Manufacturer with Id {manufacturerId} does not exist");
            }

            var products = await _productRepository.GetByManufacturerIdAsync(manufacturerId);
            if (products.Count == 0)
            {
                return Ok($"No products from manufacturer with Id {manufacturerId}");
            }

            return Ok(products);
        }

    }
}
