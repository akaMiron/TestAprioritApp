using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TestApriorit.Core.Models;
using TestApriorit.Core.Services;

namespace TestApriorit.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PositionsController : ControllerBase
    {
        private readonly IPositionService _positionService;

        public PositionsController(IPositionService positionService)
        {
            _positionService = positionService;
        }

        // GET: api/Positions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Position>>> GetPositions()
        {
            var positions = await _positionService.GetAllPositions();

            return positions.ToList();
        }

        // GET: api/Position/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Position>> GetPosition(int id)
        {
            var position = await _positionService.GetPositionById(id);

            if (position == null)
            {
                return NotFound();
            }

            return position;
        }

        // POST
        [HttpPost]
        public async Task<IActionResult> CreatePosition(Position position)
        {
            await _positionService.CreatePosition(position);

            return CreatedAtAction("GetPosition", new { id = position.PositionId }, position);
        }

        // PUT
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePosition(int id, Position position)
        {
            var positionToBeUpdated = await _positionService.GetPositionById(id);

            if (positionToBeUpdated == null)
            {
                return NotFound();
            }

            await _positionService.UpdatePosition(positionToBeUpdated, position);

            return NoContent();
        }

        // DELETE
        [HttpDelete("{id}")]
        public async Task<ActionResult<Position>> DeletePosition(int id)
        {
            Position position = await _positionService.GetPositionById(id);

            if (position == null)
            {
                return NotFound();
            }

            await _positionService.DeletePosition(position);

            return position;
        }

    }
}
