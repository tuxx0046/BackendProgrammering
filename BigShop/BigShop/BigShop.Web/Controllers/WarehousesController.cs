using BigShop.Models.Warehouse;
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

        public WarehousesController(IWarehouseRepository warehouseRepository,
                                    IZipRepository zipRepository,
                                    IWarehouse_ProductRepository warehouse_ProductRepository,
                                    IDepartmentRepository departmentRepository)
        {
            _warehouseRepository = warehouseRepository;
            _zipRepository = zipRepository;
            _warehouse_ProductRepository = warehouse_ProductRepository;
            _departmentRepository = departmentRepository;
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
            var warehouses = await _warehouseRepository.GetAllAsync();
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
    }
}
