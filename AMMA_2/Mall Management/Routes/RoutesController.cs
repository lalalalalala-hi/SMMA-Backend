using AMMAAPI.Models;
using AMMAAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace AMMAAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoutesController : ControllerBase
    {
        private readonly RoutesService _routeService;
        public RoutesController(RoutesService routeService) =>
            _routeService = routeService;

        [HttpGet]
        public async Task<ActionResult<List<Routes>>> Get() =>
            await _routeService.GetAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<Routes>> Get(string id)
        {
            var r = await _routeService.GetByIdAsync(id);

            if (r == null)
            {
                return NotFound();
            }

            return r;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Routes r)
        {
            var existingRoute = await _routeService.GetRouteByStartAndEndAsync(r.startRoute, r.endRoute);

            if (existingRoute != null)
            {
                existingRoute.count += 1;
                await _routeService.UpdateAsync(existingRoute);
                return Ok(existingRoute);
            }
            else
            {
                r.RouteId = ObjectId.GenerateNewId().ToString().Substring(0, 24);
                r.count = 1; // Initialize the count to 1
                await _routeService.CreateAsync(r);
                return CreatedAtAction(nameof(Get), new { id = r.RouteId }, r);
            }
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, Routes rIn)
        {
            var r = _routeService.GetByIdAsync;

            if (r == null)
            {
                return NotFound();
            }

            await _routeService.UpdateAsync(rIn);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var r = await _routeService.GetByIdAsync(id);

            if (r == null)
            {
                return NotFound();
            }

            await _routeService.RemoveAsync(r);

            return Ok();
        }
    }
}
