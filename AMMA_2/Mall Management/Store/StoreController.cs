using AMMAAPI.Models;
using AMMAAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System.Net.Http.Headers;

namespace AMMAAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class StoreController: ControllerBase
    {
        private readonly StoreService _storeService;
        public StoreController(StoreService storeService) =>
            _storeService = storeService;

        [HttpGet]
        public async Task<ActionResult<List<Store>>> Get() =>
            await _storeService.GetAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<Store>> Get(string id)
        {
            var s = await _storeService.GetByIdAsync(id);

            if (s == null)
            {
                return NotFound();
            }

            return s;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Store s)
        {
            if (string.IsNullOrEmpty(s.Image))
            {
                return BadRequest("Image filename is required.");
            }

            s.StoreId = ObjectId.GenerateNewId().ToString().Substring(0, 24);
            await _storeService.CreateAsync(s);

            return CreatedAtAction(nameof(Get), new { id = s.StoreId }, s);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, Store sIn)
        {
            var s = _storeService.GetByIdAsync;

            if (s == null)
            {
                return NotFound();
            }

            await _storeService.UpdateAsync(sIn);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var s = await _storeService.GetByIdAsync(id);

            if (s == null)
            {
                return NotFound();
            }

            await _storeService.RemoveAsync(s);

            return Ok();
        }
    }
}
