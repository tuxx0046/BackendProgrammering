using BigShop.Models.OrderProcess;
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
    public class OrderProcessesController : ControllerBase
    {
        private readonly IOrderProcessRepository _orderProcessRepository;
        private readonly ICustomerOrderRepository _customerOrderRepository;

        public OrderProcessesController(IOrderProcessRepository orderProcessRepository,
                                        ICustomerOrderRepository customerOrderRepository)
        {
            _orderProcessRepository = orderProcessRepository;
            _customerOrderRepository = customerOrderRepository;
        }

        [HttpPost("customerorders/{customerOrderId}")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(OrderProcess))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> CreateManufacturer(int customerOrderId, [FromBody] OrderProcessCreate orderProcessCreate)
        {
            if (customerOrderId != orderProcessCreate.CustomerOrder_Id)
            {
                return BadRequest("Id does not match");
            }

            if (ModelState.IsValid == false)
            {
                return StatusCode(400, ModelState);
            }

            // Check order existence
            var customerOrder = await _customerOrderRepository.GetByIdAsync(orderProcessCreate.CustomerOrder_Id);
            if (customerOrder == null)
            {
                return NotFound($"No order with Id {orderProcessCreate.CustomerOrder_Id} exists");
            }

            // Check order process status is not there
            var orderProcesses = await _orderProcessRepository.GetByCustomerOrderIdAsync(orderProcessCreate.CustomerOrder_Id);
            foreach (OrderProcess orderProcess in orderProcesses)
            {
                if (orderProcess.OrderStatus_Id == orderProcessCreate.OrderStatus_Id)
                {
                    return BadRequest("Customer order has already been through that process state");
                }
            }

            int affectedRows = await _orderProcessRepository.CreateAsync(orderProcessCreate);
            if (affectedRows == 1)
            {
                return StatusCode(StatusCodes.Status201Created);
            }

            return StatusCode(500);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<OrderProcess>>> GetAllOrderProcesses()
        {
            var orderProcesses = await _orderProcessRepository.GetAllAsync();
            if (orderProcesses == null)
            {
                return StatusCode(500);
            }

            if (orderProcesses.Count == 0)
            {
                return Ok("No orders in process");
            }

            return Ok(orderProcesses);
        }

        [HttpGet("customerorders/{customerOrderId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<OrderProcess>>> GetOrderProcessesByCustomerOrderId(int customerOrderId)
        {
            var customerOrder = await _customerOrderRepository.GetByIdAsync(customerOrderId);
            if (customerOrder == null)
            {
                return NotFound($"No order with Id {customerOrderId} exists");
            }

            var orderProcesses = await _orderProcessRepository.GetByCustomerOrderIdAsync(customerOrderId);
            if (orderProcesses == null)
            {
                return StatusCode(500);
            }

            if (orderProcesses.Count == 0)
            {
                return NotFound($"No orders with Id {customerOrderId} in process");
            }

            return Ok(orderProcesses);
        }
    }
}
