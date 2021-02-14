using BigShop.Models.Employee;
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
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IOrderProcessRepository _orderProcessRepository;

        public EmployeesController(IEmployeeRepository employeeRepository, IOrderProcessRepository orderProcessRepository)
        {
            _employeeRepository = employeeRepository;
            _orderProcessRepository = orderProcessRepository;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Employee))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> CreateEmployee([FromBody] EmployeeCreate employeeCreate)
        {
            if (ModelState.IsValid == false)
            {
                return StatusCode(400, ModelState);
            }

            int newEmployeeId = await _employeeRepository.CreateAsync(employeeCreate);
            if (newEmployeeId != -1)
            {
                var newEmployee = await _employeeRepository.GetByIdAsync(newEmployeeId);

                return CreatedAtRoute("GetByEmployeeId", new { employeeId = newEmployeeId }, newEmployee);
            }

            return StatusCode(500);
        }

        [HttpDelete("{employeeId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteEmployee(int employeeId)
        {
            var employee = await _employeeRepository.GetByIdAsync(employeeId);
            if (employee == null)
            {
                return NotFound($"Employee with Id {employeeId} does not exist");
            }

            var orderProcesses = await _orderProcessRepository.GetByEmployeeIdAsync(employeeId);
            if (orderProcesses.Count != 0)
            {
                return BadRequest("Cannot remove employee who has processed orders");
            }

            int affectedRows = await _employeeRepository.DeleteAsync(employeeId);
            if (affectedRows == 1)
            {
                return NoContent();
            }

            return StatusCode(500);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<Employee>>> GetAllEmployees()
        {
            var employees = await _employeeRepository.GetAllAsync();
            if (employees == null)
            {
                return StatusCode(500);
            }

            if (employees.Count == 0)
            {
                return Ok("No employees in database");
            }

            return Ok(employees);
        }

        [HttpGet("{employeeId}", Name = "GetByEmployeeId")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Employee>> GetByEmployeeId(int employeeId)
        {
            var employee = await _employeeRepository.GetByIdAsync(employeeId);
            if (employee == null)
            {
                return NotFound($"Employee with Id {employeeId} does not exist");
            }
            return Ok(employee);
        }

        [HttpPut("{employeeId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> UpdateEmployee(int employeeId, [FromBody] Employee updatedEmployee)
        {
            if (employeeId != updatedEmployee.Id)
            {
                return BadRequest("Id does not match");
            }

            var oldEmployee = await _employeeRepository.GetByIdAsync(employeeId);
            if (oldEmployee == null)
            {
                return NotFound($"Employee with Id {employeeId} not found");
            }

            if (ModelState.IsValid == false)
                return BadRequest(ModelState);

            int affectedRows = await _employeeRepository.UpdateAsync(updatedEmployee);

            if (affectedRows == 1)
            {
                return Ok();
            }

            return StatusCode(500);
        }
    }
}
