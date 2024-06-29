using AMMAAPI.Models;
using AMMAAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace AMMAAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FloorController: ControllerBase
    {
        private readonly FloorService _floorService;

        public FloorController(FloorService floorService) =>
            _floorService = floorService;

        [HttpGet]
        public async Task<ActionResult<List<Floor>>> Get() =>
            await _floorService.GetAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<Floor>> Get(string id)
        {
            var f = await _floorService.GetByIdAsync(id);

            if (f == null)
            {
                return NotFound();
            }

            return f;
        }

        [HttpPost]
        public async Task<IActionResult> Post(Floor f)
        {
            f.FloorId = ObjectId.GenerateNewId().ToString().Substring(0, 24);

            await _floorService.CreateAsync(f);

            return CreatedAtAction(nameof(Get), new { id = f.FloorId }, f);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, Floor fIn)
        {
            var f = _floorService.GetByIdAsync;

            if (f == null)
            {
                return NotFound();
            }

            await _floorService.UpdateAsync(fIn);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var f = await _floorService.GetByIdAsync(id);

            if (f == null)
            {
                return NotFound();
            }

            await _floorService.RemoveAsync(f);

            return NoContent();
        }
    }
}
