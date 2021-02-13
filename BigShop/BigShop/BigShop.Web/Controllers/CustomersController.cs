using BigShop.Models.Customer;
using BigShop.Models.CustomerOrder;
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
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ICustomerOrderRepository _customerOrderRepository;

        public CustomersController(ICustomerRepository customerRepository,
                                   ICustomerOrderRepository customerOrderRepository)
        {
            _customerRepository = customerRepository;
            _customerOrderRepository = customerOrderRepository;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Customer))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> CreateCountry([FromBody] CustomerCreate customerCreate)
        {
            if (ModelState.IsValid == false)
            {
                return StatusCode(400, ModelState);
            }

            int newCustomerId = await _customerRepository.CreateAsync(customerCreate);
            if (newCustomerId != -1)
            {
                var newCustomer = await _customerRepository.GetByIdAsync(newCustomerId);

                return CreatedAtRoute("GetByCustomerId", new { customerId = newCustomerId }, newCustomer);
            }

            return StatusCode(500);
        }

        [HttpDelete("{customerId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteCustomer(int customerId)
        {
            var customer = await _customerRepository.GetByIdAsync(customerId);
            if (customer == null)
            {
                return NotFound($"Customer with Id {customerId} does not exist");
            }

            var customerOrders = await _customerOrderRepository.GetAllByCustomerId(customerId);
            if (customerOrders.Count != 0)
            {
                return BadRequest("Cannot remove customers with orders");
            }

            int affectedRows = await _customerRepository.DeleteAsync(customerId);
            if (affectedRows > 0)
            {
                return NoContent();
            }

            return StatusCode(500);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<Customer>>> GetAllCustomers()
        {
            var customers = await _customerRepository.GetAllAsync();
            if (customers == null)
            {
                return StatusCode(500);
            }

            if (customers.Count == 0)
            {
                return Ok("No customers in database");
            }

            return Ok(customers);
        }

        [HttpGet("{customerId}", Name = "GetByCustomerId")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Customer>> GetByCustomerId(int customerId)
        {
            var customer = await _customerRepository.GetByIdAsync(customerId);
            if (customer == null)
            {
                return NotFound($"Customer with Id {customerId} does not exist");
            }
            return Ok(customer);
        }

        [HttpPut("{customerId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> UpdateCountry(int customerId, [FromBody] Customer updatedCustomer)
        {
            if (customerId != updatedCustomer.Id)
            {
                return BadRequest("Id does not match");
            }

            var oldCustomer = await _customerRepository.GetByIdAsync(customerId);
            if (oldCustomer == null)
            {
                return NotFound($"Customer with Id {customerId} not found");
            }

            if (ModelState.IsValid == false)
                return BadRequest(ModelState);

            int affectedRows = await _customerRepository.UpdateAsync(updatedCustomer);

            if (affectedRows == 1)
            {
                return Ok();
            }

            return StatusCode(500);
        }

        [HttpGet("{customerId}/orders")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<CustomerOrder>>> GetOrdersByCustomerId(int customerId)
        {
            var customer = await _customerRepository.GetByIdAsync(customerId);
            if (customer == null)
            {
                return NotFound($"Customer with Id {customerId} not found");
            }

            var customerOrders = await _customerOrderRepository.GetAllByCustomerId(customerId);
            if (customerOrders.Count == 0)
            {
                return Ok($"Could not find orders made by customer with Id {customerId}");
            }

            return Ok(customerOrders);
        }
    }
}
