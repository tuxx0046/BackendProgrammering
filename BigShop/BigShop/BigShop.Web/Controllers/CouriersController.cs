using BigShop.Models.Courier;
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
    public class CouriersController : ControllerBase
    {
        private readonly ICourierRepository _courierRepository;

        public CouriersController(ICourierRepository courierRepository)
        {
            _courierRepository = courierRepository;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Courier))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> CreateCourier([FromBody] CourierCreate courierCreate)
        {
            if (ModelState.IsValid == false)
            {
                return StatusCode(400, ModelState);
            }

            int newCourierId = await _courierRepository.CreateAsync(courierCreate);
            if (newCourierId != -1)
            {
                var newCourier = await _courierRepository.GetByIdAsync(newCourierId);

                return CreatedAtRoute("GetByCourierId", new { courierId = newCourierId }, newCourier);
            }

            return StatusCode(500);
        }

        // Delete not allowed because Couriers are referenced to in customer orders

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<Courier>>> GetAllCouriers()
        {
            var countries = await _courierRepository.GetAllAsync();
            if (countries == null)
            {
                return StatusCode(500);
            }

            if (countries.Count == 0)
            {
                return Ok("No couriers exist in database");
            }

            return Ok(countries);
        }

        [HttpGet("{courierId}", Name = "GetByCourierId")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Courier>> GetByCountryId(int courierId)
        {
            var courier = await _courierRepository.GetByIdAsync(courierId);
            if (courier == null)
            {
                return NotFound($"Courier with Id {courierId} does not exist");
            }
            return Ok(courier);
        }

        [HttpPut("{courierId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> UpdateCourier(int courierId, [FromBody] Courier updatedCourier)
        {
            if (courierId != updatedCourier.Id)
            {
                return BadRequest("Id does not match");
            }

            var oldCourier = await _courierRepository.GetByIdAsync(courierId);
            if (oldCourier == null)
            {
                return NotFound($"Courier with Id {courierId} not found");
            }

            if (ModelState.IsValid == false)
                return BadRequest(ModelState);

            int affectedRows = await _courierRepository.UpdateAsync(updatedCourier);

            if (affectedRows > 0)
            {
                return Ok();
            }

            return StatusCode(500);
        }
    }
}
