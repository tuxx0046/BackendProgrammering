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
    public class PaymentMethodsController : ControllerBase
    {
        private readonly IPaymentMethodRepository _paymentMethodRepository;

        public PaymentMethodsController(IPaymentMethodRepository paymentMethodRepository)
        {
            _paymentMethodRepository = paymentMethodRepository;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(PaymentMethod))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> CreatePaymentMethod([FromBody] PaymentMethodCreate paymentMethodCreate)
        {
            if (ModelState.IsValid == false)
            {
                return StatusCode(400, ModelState);
            }

            int newPaymentMethodId = await _paymentMethodRepository.CreateAsync(paymentMethodCreate);
            if (newPaymentMethodId != -1)
            {
                var newPaymentMethod = await _paymentMethodRepository.GetByIdAsync(newPaymentMethodId);

                return CreatedAtRoute("GetByPaymentMethodId", new { paymentMethodId = newPaymentMethodId }, newPaymentMethod);
            }

            return StatusCode(500);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<PaymentMethod>>> GetAllPaymentMethods()
        {
            var paymentMethods = await _paymentMethodRepository.GetAllAsync();
            if (paymentMethods == null)
            {
                return StatusCode(500);
            }

            if (paymentMethods.Count == 0)
            {
                return Ok("No payment methods registered in database");
            }

            return Ok(paymentMethods);
        }

        [HttpGet("{paymentMethodId}", Name = "GetByPaymentMethodId")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PaymentMethod>> GetByPaymentMethodId(int paymentMethodId)
        {
            var paymentMethod = await _paymentMethodRepository.GetByIdAsync(paymentMethodId);
            if (paymentMethod == null)
            {
                return NotFound($"Payment method with Id {paymentMethodId} does not exist");
            }
            return Ok(paymentMethod);
        }

        [HttpPut("{paymentMethodId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> UpdatePaymentMethod(int paymentMethodId, [FromBody] PaymentMethod updatedPaymentMethod)
        {
            if (paymentMethodId != updatedPaymentMethod.Id)
            {
                return BadRequest("Id does not match");
            }

            var oldPaymentMethod = await _paymentMethodRepository.GetByIdAsync(paymentMethodId);
            if (oldPaymentMethod == null)
            {
                return NotFound($"Payment method with Id {paymentMethodId} not found");
            }

            if (ModelState.IsValid == false)
                return BadRequest(ModelState);

            int affectedRows = await _paymentMethodRepository.UpdateAsync(updatedPaymentMethod);

            if (affectedRows == 1)
            {
                return Ok();
            }

            return StatusCode(500);
        }
    }
}
