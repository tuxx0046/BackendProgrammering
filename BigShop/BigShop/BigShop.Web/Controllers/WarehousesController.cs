using BigShop.Models.Warehouse;
using BigShop.Models.Warehouse_Product;
using BigShop.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BigShop.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WarehousesController : ControllerBase
    {
        private readonly IWarehouseRepository _warehouseRepository;
        private readonly IZipRepository _zipRepository;
        private readonly IWarehouse_ProductRepository _warehouse_ProductRepository;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IProductRepository _productRepository;

        public WarehousesController(IWarehouseRepository warehouseRepository,
                                    IZipRepository zipRepository,
                                    IWarehouse_ProductRepository warehouse_ProductRepository,
                                    IDepartmentRepository departmentRepository,
                                    IProductRepository productRepository)
        {
            _warehouseRepository = warehouseRepository;
            _zipRepository = zipRepository;
            _warehouse_ProductRepository = warehouse_ProductRepository;
            _departmentRepository = departmentRepository;
            _productRepository = productRepository;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Warehouse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> CreateWarehouse([FromBody] WarehouseCreate warehouseCreate)
        {
            if (ModelState.IsValid == false)
            {
                return StatusCode(400, ModelState);
            }

            var zip = await _zipRepository.GetByIdAsync(warehouseCreate.Zip_Id);
            if (zip == null)
            {
                return BadRequest("No zip codes with that Id");
            }

            int newWarehouseId = await _warehouseRepository.CreateAsync(warehouseCreate);
            if (newWarehouseId != -1)
            {
                var newWarehouse = await _warehouseRepository.GetByIdAsync(newWarehouseId);

                return CreatedAtRoute("GetByWarehouseId", new { warehouseId = newWarehouseId }, newWarehouse);
            }

            return StatusCode(500);
        }

        [HttpDelete("{warehouseId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteWarehouse(int warehouseId)
        {
            var warehouse = await _warehouseRepository.GetByIdAsync(warehouseId);
            if (warehouse == null)
            {
                return NotFound($"Warehouse with Id {warehouseId} does not exist");
            }

            var departments = await _departmentRepository.GetByWarehouseIdAsync(warehouseId);
            if (departments.Count != 0)
            {
                return BadRequest("Cannot remove warehouse with departments are attached");
            }

            await _warehouse_ProductRepository.DeleteByWarehouseIdAsync(warehouseId);
            int affectedRows = await _warehouseRepository.DeleteAsync(warehouseId);
            if (affectedRows == 1)
            {
                return NoContent();
            }

            return StatusCode(500);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<Warehouse>>> GetAllWarehouses()
        {
            ICollection<Warehouse> warehouses = await _warehouseRepository.GetAllAsync();
            if (warehouses == null)
            {
                return StatusCode(500);
            }

            if (warehouses.Count == 0)
            {
                return Ok("No warehouses in database");
            }
            
            return Ok(warehouses);
        }

        [HttpGet("{warehouseId}", Name = "GetByWarehouseId")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Warehouse>> GetByWarehouseId(int warehouseId)
        {
            var warehouse = await _warehouseRepository.GetByIdAsync(warehouseId);
            if (warehouse == null)
            {
                return NotFound($"Warehouse with Id {warehouseId} does not exist");
            }
            return Ok(warehouse);
        }

        [HttpPut("{warehouseId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> UpdateWarehouse(int warehouseId, [FromBody] Warehouse updatedWarehouse)
        {
            if (warehouseId != updatedWarehouse.Id)
            {
                return BadRequest("Warehouse Id does not match");
            }

            var zip = await _zipRepository.GetByIdAsync(updatedWarehouse.Zip_Id);
            if (zip == null)
            {
                return BadRequest("No zip code with that Id");
            }

            var oldWarehouse = await _warehouseRepository.GetByIdAsync(warehouseId);
            if (oldWarehouse == null)
            {
                return NotFound($"Warehouse with Id {warehouseId} not found");
            }

            if (ModelState.IsValid == false)
                return BadRequest(ModelState);

            int affectedRows = await _warehouseRepository.UpdateAsync(updatedWarehouse);

            if (affectedRows == 1)
            {
                return Ok();
            }

            return StatusCode(500);
        }

        [HttpGet("{warehouseId}/products")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<Warehouse_Product>>> GetWarehouseProducts(int warehouseId)
        {
            var warehouse = await _warehouseRepository.GetByIdAsync(warehouseId);
            if (warehouse == null)
            {
                return NotFound($"Warehouse with Id {warehouseId} does not exist");
            }

            var warehouseProducts = await _warehouse_ProductRepository.GetByWarehouseIdAsync(warehouseId);
            if (warehouseProducts.Count == 0)
            {
                return BadRequest("Warehouse has no products");
            }
            return Ok(warehouseProducts);
        }

        [HttpGet("{warehouseId}/products/{productId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Warehouse_Product>> GetProductQuantity(int warehouseId, int productId)
        {
            var warehouse = await _warehouseRepository.GetByIdAsync(warehouseId);
            if (warehouse == null)
            {
                return NotFound($"Warehouse with Id {warehouseId} does not exist");
            }

            var product = await _productRepository.GetByIdAsync(productId);
            if (product == null)
            {
                return BadRequest("Product doesn't exist");
            }

            var warehouseProduct = await _warehouse_ProductRepository.GetByProductIdAndWarehouseIdAsync(productId,
                                                                                                        warehouseId);
            if (warehouseProduct == null)
            {
                return NotFound();
            }
            return Ok(warehouseProduct);
        }

        [HttpPut("{warehouseId}/products/{productId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> UpdateProductQuantity(int warehouseId, 
                                                              int productId, 
                                                              [FromBody] Warehouse_Product warehouse_Product)
        {
            if (warehouse_Product.Quantity < 0)
            {
                return BadRequest("Quantity cannot be negative");
            }

            if (warehouseId != warehouse_Product.Warehouse_Id || productId != warehouse_Product.Warehouse_Id)
            {
                return BadRequest("Ids not matching endpoint");
            }

            var warehouse = await _warehouseRepository.GetByIdAsync(warehouseId);
            if (warehouse == null)
            {
                return NotFound($"Warehouse with Id {warehouseId} does not exist");
            }

            var product = await _productRepository.GetByIdAsync(productId);
            if (product == null)
            {
                return BadRequest("Product doesn't exist");
            }

            int affectedRows = await _warehouse_ProductRepository.UpdateQuantityAsync(warehouse_Product);
            if (affectedRows == 1)
            {
                return Ok();
            }

            return StatusCode(500);
        }
    }
}
