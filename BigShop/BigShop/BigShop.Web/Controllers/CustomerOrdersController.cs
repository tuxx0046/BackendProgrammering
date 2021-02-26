using BigShop.Models.Courier;
using BigShop.Models.Customer;
using BigShop.Models.CustomerOrder;
using BigShop.Models.OrderLine;
using BigShop.Models.OrderProcess;
using BigShop.Models.PaymentMethod;
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
    public class CustomerOrdersController : ControllerBase
    {
        private readonly ICustomerOrderRepository _customerOrderRepository;
        private readonly IOrderProcessRepository _orderProcessRepository;
        private readonly ICourierRepository _courierRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IPaymentMethodRepository _paymentMethodRepository;
        private readonly IOrderLineRepository _orderLineRepository;
        private readonly IProductRepository _productRepository;

        public CustomerOrdersController(ICustomerOrderRepository customerOrderRepository,
                                        IOrderProcessRepository orderProcessRepository,
                                        ICourierRepository courierRepository,
                                        ICustomerRepository customerRepository,
                                        IPaymentMethodRepository paymentMethodRepository,
                                        IOrderLineRepository orderLineRepository,
                                        IProductRepository productRepository)
        {
            _customerOrderRepository = customerOrderRepository;
            _orderProcessRepository = orderProcessRepository;
            _courierRepository = courierRepository;
            _customerRepository = customerRepository;
            _paymentMethodRepository = paymentMethodRepository;
            _orderLineRepository = orderLineRepository;
            _productRepository = productRepository;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CustomerOrder))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> CreateCustomerOrder([FromBody] CustomerOrderClientCreate customerOrderClientCreate)
        {
            if (ModelState.IsValid == false)
            {
                return StatusCode(400, ModelState);
            }

            if (customerOrderClientCreate.OrderLines.Count == 0)
            {
                return BadRequest("No products have been ordered");
            }

            Customer customer = await _customerRepository.GetByIdAsync(customerOrderClientCreate.Customer_Id);
            if (customer == null)
            {
                return BadRequest("The chosen customer doesn't exist");
            }

            PaymentMethod paymentMethod = await _paymentMethodRepository.GetByIdAsync(customerOrderClientCreate.PaymentMethod_Id);
            if (paymentMethod == null)
            {
                return BadRequest("The chosen payment method doesn't exist");
            }

            Courier courier = await _courierRepository.GetByIdAsync(customerOrderClientCreate.Courier_Id);
            if (courier == null)
            {
                return BadRequest("The chosen courier doesn't exist");
            }

            // Ready Order line to be saved in database if everything is alright
            List<OrderLineCreate> newOrderLines = new List<OrderLineCreate>();

            foreach (var orderLine in customerOrderClientCreate.OrderLines)
            {
                var product = await _productRepository.GetByIdAsync(orderLine.Product_Id);
                if (product == null)
                {
                    return BadRequest($"The chosen product with Id {product.Id} doesn't exist");
                }
                newOrderLines.Add(new OrderLineCreate
                {
                    Quantity = orderLine.Quantity,
                    Price = product.Price,
                    Product_Id = orderLine.Product_Id
                });
            }

            CustomerOrderCreate customerOrderCreate = new CustomerOrderCreate
            {
                Customer_Id = customerOrderClientCreate.Customer_Id,
                PaymentMethod_Id = customerOrderClientCreate.PaymentMethod_Id,
                Courier_Id = customerOrderClientCreate.Courier_Id,
                InitialShippingCost = courier.InitialCost,
                WeightFee = courier.WeightFee
            };
            
            // Save customer order to database
            int newCustomerOrderId = await _customerOrderRepository.CreateAsync(customerOrderCreate);
            if (newCustomerOrderId != -1)
            {
                // Insert missing customer order id into orderlines
                foreach (var orderLine in newOrderLines)
                {
                    orderLine.CustomerOrder_Id = newCustomerOrderId;
                }

                // save orderlines to database
                int affectedOrderLineRows = await _orderLineRepository.CreateAsync(newOrderLines);
                if (affectedOrderLineRows < 1)
                {
                    await _customerOrderRepository.DeleteAsync(newCustomerOrderId);
                    return StatusCode(500);
                }

                var newCustomerOrder = await _customerOrderRepository.GetByIdAsync(newCustomerOrderId);

                OrderProcessCreate newOrderProcess = new OrderProcessCreate
                {
                    CustomerOrder_Id = newCustomerOrder.Id,
                };

                int affectedOrderProcessRows = await _orderProcessRepository.CreateAsync(newOrderProcess);
                if (affectedOrderProcessRows > 0)
                {
                    return CreatedAtRoute("GetByCustomerOrderId",
                                          new { customerOrderId = newCustomerOrderId },
                                          newCustomerOrder);
                }
                else
                {
                    await _customerOrderRepository.DeleteAsync(newCustomerOrderId);
                    await _orderLineRepository.DeleteByCustomerOrderIdAsync(newCustomerOrderId);

                    return StatusCode(500);
                }
            }

            return StatusCode(500);
        }

        [HttpDelete("{customerOrderId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteCustomerOrder(int customerOrderId)
        {
            var customerOrder = await _customerOrderRepository.GetByIdAsync(customerOrderId);
            if (customerOrder == null)
            {
                return NotFound($"Customer order with Id {customerOrderId} does not exist");
            }

            // Delete from order process and order line tables first
            int affectedOrderProcessRows = await _orderProcessRepository.DeleteByCustomerOrderIdAsync(customerOrderId);
            if (affectedOrderProcessRows <= 0)
            {
                return NotFound($"Order {customerOrderId} not in process");
            }

            int affectedOrderLineRows = await _orderLineRepository.DeleteByCustomerOrderIdAsync(customerOrderId);
            if (affectedOrderLineRows <= 0)
            {
                return NotFound($"No order lines attached to order {customerOrderId}");
            }

            int affectedCustomerOrderRows = await _customerOrderRepository.DeleteAsync(customerOrderId);
            if (affectedCustomerOrderRows > 0)
            {
                return NoContent();
            }

            return StatusCode(500);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<CustomerOrder>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<CustomerOrder>>> GetAllCustomerOrders()
        {
            var customerOrders = await _customerOrderRepository.GetAllAsync();
            if (customerOrders.Count == 0)
            {
                return StatusCode(404);
            }

            return Ok(customerOrders);
        }

        [HttpGet("{customerOrderId}", Name = "GetByCustomerOrderId")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CustomerOrder))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CustomerOrder>> GetByCategoryId(int customerOrderId)
        {
            var customerOrder = await _customerOrderRepository.GetByIdAsync(customerOrderId);
            if (customerOrder == null)
            {
                return NotFound($"Order with Id {customerOrderId} does not exist");
            }
            return Ok(customerOrder);
        }

        [HttpGet("{customerOrderId}/details")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CustomerOrder>> GetCustomerOrderDetailsByCategoryId(int customerOrderId)
        {
            var customerOrder = await _customerOrderRepository.GetByIdAsync(customerOrderId);
            if (customerOrder == null)
            {
                return NotFound($"Order with Id {customerOrderId} does not exist");
            }

            var orderLines = await _orderLineRepository.GetByCustomerOrderIdAsync(customerOrderId);
            var details = new
            {
                Order = customerOrder,
                ProductsOrdered = orderLines                
            };

            return Ok(details);
        }
    }
}