using AMMAAPI.Models;
using AMMAAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace AMMAAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserRouteController: ControllerBase
    {
        private readonly UserRouteService _userRouteService;

        public UserRouteController(UserRouteService userRouteService) =>
            _userRouteService = userRouteService;

        [HttpGet]
        public async Task<ActionResult<List<UserRoute>>> Get() =>
            await _userRouteService.GetAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<UserRoute>> Get(string id)
        {
            var r = await _userRouteService.GetByIdAsync(id);

            if (r == null)
            {
                return NotFound();
            }

            return r;
        }

        [HttpPost]
        public async Task<IActionResult> Post(UserRoute r)
        {
            r.UserRouteId = ObjectId.GenerateNewId().ToString().Substring(0, 24);

            await _userRouteService.CreateAsync(r);

            return CreatedAtAction(nameof(Get), new { id = r.UserRouteId }, r);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, UserRoute rIn)
        {
            var r = _userRouteService.GetByIdAsync;

            if (r == null)
            {
                return NotFound();
            }

            await _userRouteService.UpdateAsync(rIn);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var r = await _userRouteService.GetByIdAsync(id);

            if (r == null)
            {
                return NotFound();
            }

            await _userRouteService.RemoveAsync(r);

            return Ok();
        }
    }
}
