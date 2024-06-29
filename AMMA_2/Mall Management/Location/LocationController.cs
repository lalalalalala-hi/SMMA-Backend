using AMMAAPI.Models;
using AMMAAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace AMMAAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LocationController : ControllerBase
    {
        private readonly LocationService _locationService;

        public LocationController(LocationService locationService) =>
            _locationService = locationService;

        [HttpGet]
        public async Task<ActionResult<List<Location>>> Get() =>
            await _locationService.GetAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<Location>> Get(string id)
        {
            var l = await _locationService.GetByIdAsync(id);

            if (l == null)
            {
                return NotFound();
            }

            return l;
        }

        [HttpPost]
        public async Task<IActionResult> Post(Location l)
        {
            l.LocationId = ObjectId.GenerateNewId().ToString().Substring(0, 24);

            await _locationService.CreateAsync(l);

            return CreatedAtAction(nameof(Get), new { id = l.LocationId }, l);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, Location lIn)
        {
            var l = _locationService.GetByIdAsync;

            if (l == null)
            {
                return NotFound();
            }

            await _locationService.UpdateAsync(lIn);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var l = await _locationService.GetByIdAsync(id);

            if (l == null)
            {
                return NotFound();
            }

            await _locationService.RemoveAsync(l);

            return NoContent();
        }
    }
}
