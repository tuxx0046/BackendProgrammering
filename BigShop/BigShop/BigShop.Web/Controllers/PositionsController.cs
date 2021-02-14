using BigShop.Models.Position;
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
    public class PositionsController : ControllerBase
    {
        private readonly IPositionRepository _positionRepository;

        public PositionsController(IPositionRepository positionRepository)
        {
            _positionRepository = positionRepository;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Position))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> CreatePosition([FromBody] PositionCreate positionCreate)
        {
            if (ModelState.IsValid == false)
            {
                return StatusCode(400, ModelState);
            }

            int newPositionId = await _positionRepository.CreateAsync(positionCreate);
            if (newPositionId != -1)
            {
                var newPosition = await _positionRepository.GetByIdAsync(newPositionId);

                return CreatedAtRoute("GetByPositionId", new { positionId = newPositionId }, newPosition);
            }

            return StatusCode(500);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<Position>>> GetAllPositions()
        {
            var positions = await _positionRepository.GetAllAsync();
            if (positions == null)
            {
                return StatusCode(500);
            }

            if (positions.Count == 0)
            {
                return Ok("No positions registered in database");
            }

            return Ok(positions);
        }

        [HttpGet("{positionId}", Name = "GetByPositionId")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Position>> GetByPositionId(int positionId)
        {
            var position = await _positionRepository.GetByIdAsync(positionId);
            if (position == null)
            {
                return NotFound($"Position with Id {positionId} does not exist");
            }
            return Ok(position);
        }

        [HttpPut("{positionId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> UpdatePosition(int positionId, [FromBody] Position updatedPosition)
        {
            if (positionId != updatedPosition.Id)
            {
                return BadRequest("Id does not match");
            }

            var position = await _positionRepository.GetByIdAsync(positionId);
            if (position == null)
            {
                return NotFound($"Position with Id {positionId} not found");
            }

            if (ModelState.IsValid == false)
                return BadRequest(ModelState);

            int affectedRows = await _positionRepository.UpdateAsync(updatedPosition);

            if (affectedRows == 1)
            {
                return Ok();
            }

            return StatusCode(500);
        }
    }
}
