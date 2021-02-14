using BigShop.Models.OrderProcess;
using BigShop.Models.OrderStatus;
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
    public class OrderStatusesController : ControllerBase
    {
        private readonly IOrderStatusRepository _orderStatusRepository;
        private readonly IOrderProcessRepository _orderProcessRepository;

        public OrderStatusesController(IOrderStatusRepository orderStatusRepository,
                                       IOrderProcessRepository orderProcessRepository)
        {
            _orderStatusRepository = orderStatusRepository;
            _orderProcessRepository = orderProcessRepository;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(OrderStatus))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> CreateStatus([FromBody] OrderStatusCreate orderStatusCreate)
        {
            if (ModelState.IsValid == false)
            {
                return StatusCode(400, ModelState);
            }

            int newOrderStatusId = await _orderStatusRepository.CreateAsync(orderStatusCreate);
            if (newOrderStatusId != -1)
            {
                var newOrderStatus = await _orderStatusRepository.GetByIdAsync(newOrderStatusId);

                return CreatedAtRoute("GetByOrderStatusId", new { orderStatusId = newOrderStatusId }, newOrderStatus);
            }

            return StatusCode(500);
        }

        [HttpDelete("{orderStatusId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteOrderStatus(int orderStatusId)
        {
            var orderStatus = await _orderStatusRepository.GetByIdAsync(orderStatusId);
            if (orderStatus == null)
            {
                return NotFound($"Order status with Id {orderStatusId} does not exist");
            }

            var orderProcesses = await _orderProcessRepository.GetByOrderStatusIdAsync(orderStatusId);
            if (orderProcesses.Count != 0)
            {
                return BadRequest("Cannot remove status due to order process assignment");
            }

            int affectedRows = await _orderStatusRepository.DeleteAsync(orderStatusId);
            if (affectedRows == 1)
            {
                return NoContent();
            }

            return StatusCode(500);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<OrderStatus>>> GetAllOrderStatuses()
        {
            var orderStatuses = await _orderStatusRepository.GetAllAsync();
            if (orderStatuses == null)
            {
                return StatusCode(500);
            }

            if (orderStatuses.Count == 0)
            {
                return Ok("No order statuses in database");
            }

            return Ok(orderStatuses);
        }

        [HttpGet("{orderStatusId}", Name = "GetByOrderStatusId")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<OrderStatus>> GetByOrderStatusId(int orderStatusId)
        {
            var orderStatus = await _orderStatusRepository.GetByIdAsync(orderStatusId);
            if (orderStatus == null)
            {
                return NotFound($"Order status with Id {orderStatusId} does not exist");
            }
            return Ok(orderStatus);
        }

        [HttpPut("{orderStatusId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> UpdateOrderStatus(int orderStatusId, [FromBody] OrderStatus updatedOrderStatus)
        {
            if (orderStatusId != updatedOrderStatus.Id)
            {
                return BadRequest("Id does not match");
            }

            var oldOrderStatus = await _orderStatusRepository.GetByIdAsync(orderStatusId);
            if (oldOrderStatus == null)
            {
                return NotFound($"Order status with Id {orderStatusId} not found");
            }

            if (ModelState.IsValid == false)
                return BadRequest(ModelState);

            int affectedRows = await _orderStatusRepository.UpdateAsync(updatedOrderStatus);

            if (affectedRows == 1)
            {
                return Ok();
            }

            return StatusCode(500);
        }

        [HttpGet("{orderStatusId}/orderprocesses")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<OrderProcess>>> GetOrderProcessesByOrderStatusId(int orderStatusId)
        {
            var orderStatus = await _orderStatusRepository.GetByIdAsync(orderStatusId);
            if (orderStatus == null)
            {
                return NotFound($"Order Status with Id {orderStatusId} does not exist");
            }

            var orderProcesses = await _orderProcessRepository.GetByOrderStatusIdAsync(orderStatusId);
            if (orderProcesses.Count == 0)
            {
                return Ok($"No orders processed with order status Id {orderStatusId}");
            }

            return Ok(orderProcesses);
        }
    }
}
