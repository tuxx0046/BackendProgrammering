using BigShop.Models.Customer;
using BigShop.Models.Warehouse;
using BigShop.Models.Zip;
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
    public class ZipsController : ControllerBase
    {
        private readonly IZipRepository _zipRepository;
        private readonly ICountryRepository _countryRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IWarehouseRepository _warehouseRepository;

        public ZipsController(IZipRepository zipRepository,
                              ICountryRepository countryRepository,
                              ICustomerRepository customerRepository,
                              IWarehouseRepository warehouseRepository)
        {
            _zipRepository = zipRepository;
            _countryRepository = countryRepository;
            _customerRepository = customerRepository;
            _warehouseRepository = warehouseRepository;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Zip))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> CreateZip([FromBody] ZipCreate zipCreate)
        {
            if (ModelState.IsValid == false)
            {
                return StatusCode(400, ModelState);
            }

            var country = await _countryRepository.GetByIdAsync(zipCreate.Country_Id);
            if (country == null)
            {
                return BadRequest("No country with that Id");
            }

            int newZipId = await _zipRepository.CreateAsync(zipCreate);
            if (newZipId != -1)
            {
                var newZip = await _zipRepository.GetByIdAsync(newZipId);

                return CreatedAtRoute("GetByZipId", new { zipId = newZipId }, newZip);
            }

            return StatusCode(500);
        }

        [HttpDelete("{zipId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteZip(int zipId)
        {
            var zip = await _zipRepository.GetByIdAsync(zipId);
            if (zip == null)
            {
                return NotFound($"Zip with Id {zipId} does not exist");
            }

            ICollection<Customer> customers = await _customerRepository.GetAllAsync();
            var customersWithCurrentZip = customers.Where(c => c.Zip_Id == zipId).ToList();
            if (customersWithCurrentZip.Count != 0)
            {
                return BadRequest("Cannot remove zip code when customers are using them");
            }

            ICollection<Warehouse> warehouses = await _warehouseRepository.GetAllAsync();
            var warehousesWithCurrentZip = warehouses.Where(w => w.Zip_Id == zipId).ToList();
            if (warehousesWithCurrentZip.Count != 0)
            {
                return BadRequest("Cannot remove zip code when warehouses are using them");
            }

            int affectedRows = await _zipRepository.DeleteAsync(zipId);
            if (affectedRows == 1)
            {
                return NoContent();
            }

            return StatusCode(500);
        }
                
        [HttpGet("{zipId}", Name = "GetByZipId")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Zip>> GetByZipId(int zipId)
        {
            var zip = await _zipRepository.GetByIdAsync(zipId);
            if (zip == null)
            {
                return NotFound($"Zip with Id {zipId} does not exist");
            }
            return Ok(zip);
        }

        [HttpPut("{zipId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> UpdateZip(int zipId, [FromBody] Zip updatedZip)
        {
            if (zipId != updatedZip.Id)
            {
                return BadRequest("Zip Id does not match");
            }

            var country = await _countryRepository.GetByIdAsync(updatedZip.Country_Id);
            if (country == null)
            {
                return BadRequest("No country code with that Id");
            }

            var oldZip = await _zipRepository.GetByIdAsync(zipId);
            if (oldZip == null)
            {
                return NotFound($"Zip code with Id {zipId} not found");
            }

            if (ModelState.IsValid == false)
                return BadRequest(ModelState);

            int affectedRows = await _zipRepository.UpdateAsync(updatedZip);

            if (affectedRows == 1)
            {
                return Ok();
            }

            return StatusCode(500);
        }
    }
}
