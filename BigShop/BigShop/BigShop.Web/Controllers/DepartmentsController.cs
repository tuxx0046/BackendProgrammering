using BigShop.Models.Department;
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
    public class DepartmentsController : ControllerBase
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IEmployeeRepository _employeeRepository;

        public DepartmentsController(IDepartmentRepository departmentRepository,
                                     IEmployeeRepository employeeRepository)
        {
            _departmentRepository = departmentRepository;
            _employeeRepository = employeeRepository;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Department))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> CreateDepartment([FromBody] DepartmentCreate departmentCreate)
        {
            if (ModelState.IsValid == false)
            {
                return StatusCode(400, ModelState);
            }

            int newDepartmentId = await _departmentRepository.CreateAsync(departmentCreate);
            if (newDepartmentId != -1)
            {
                var newDepartment = await _departmentRepository.GetByIdAsync(newDepartmentId);

                return CreatedAtRoute("GetByDepartmentId", new { departmentId = newDepartmentId }, newDepartment);
            }

            return StatusCode(500);
        }

        [HttpDelete("{departmentId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteDepartment(int departmentId)
        {
            var department = await _departmentRepository.GetByIdAsync(departmentId);
            if (department == null)
            {
                return NotFound($"Department with Id {departmentId} does not exist");
            }

            var employees = await _employeeRepository.GetByDepartmentIdAsync(departmentId);
            if (employees.Count != 0)
            {
                return BadRequest("Cannot remove departments with employees attached");
            }

            int affectedRows = await _departmentRepository.DeleteAsync(departmentId);
            if (affectedRows == 1)
            {
                return NoContent();
            }

            return StatusCode(500);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<Department>>> GetAllDepartments()
        {
            var departments = await _departmentRepository.GetAllAsync();
            if (departments == null)
            {
                return StatusCode(500);
            }

            if (departments.Count == 0)
            {
                return Ok("No departments in database");
            }

            return Ok(departments);
        }

        [HttpGet("{departmentId}", Name = "GetByDepartmentId")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Department>> GetByDepartmentId(int departmentId)
        {
            var department = await _departmentRepository.GetByIdAsync(departmentId);
            if (department == null)
            {
                return NotFound($"Department with Id {departmentId} does not exist");
            }
            return Ok(department);
        }

        [HttpPut("{departmentId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> UpdateDepartment(int departmentId, [FromBody] Department updatedDepartment)
        {
            if (departmentId != updatedDepartment.Id)
            {
                return BadRequest("Id does not match");
            }

            var oldDepartment = await _departmentRepository.GetByIdAsync(departmentId);
            if (oldDepartment == null)
            {
                return NotFound($"Department with Id {departmentId} not found");
            }

            if (ModelState.IsValid == false)
                return BadRequest(ModelState);

            int affectedRows = await _departmentRepository.UpdateAsync(updatedDepartment);

            if (affectedRows == 1)
            {
                return Ok();
            }

            return StatusCode(500);
        }

        [HttpGet("{departmentId}/employees")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<Employee>>> GetEmployeesByDepartmentId(int departmentId)
        {
            var department = await _departmentRepository.GetByIdAsync(departmentId);
            if (department == null)
            {
                return NotFound($"Department with Id {departmentId} not found");
            }

            var employees = await _employeeRepository.GetByDepartmentIdAsync(departmentId);
            if (employees.Count == 0)
            {
                return Ok($"No employees in department with Id {departmentId}");
            }

            return Ok(employees);
        }
    }
}
