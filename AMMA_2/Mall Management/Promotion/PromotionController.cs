using AMMAAPI.Models;
using AMMAAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace AMMAAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PromotionController: ControllerBase
    {
        private readonly PromotionService _promotionService;

        public PromotionController(PromotionService promotionService) =>
            _promotionService = promotionService;

        [HttpGet]
        public async Task<ActionResult<List<Promotion>>> Get() =>
            await _promotionService.GetAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<Promotion>> Get(string id)
        {
            var p = await _promotionService.GetByIdAsync(id);

            if (p == null)
            {
                return NotFound();
            }

            return p;
        }

        [HttpPost]
        public async Task<IActionResult> Post(Promotion p)
        {
            p.PromotionId = ObjectId.GenerateNewId().ToString().Substring(0, 24);

            await _promotionService.CreateAsync(p);

            return CreatedAtAction(nameof(Get), new { id = p.PromotionId }, p);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, Promotion pIn)
        {
            var p = _promotionService.GetByIdAsync;

            if (p == null)
            {
                return NotFound();
            }

            await _promotionService.UpdateAsync(pIn);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var p = await _promotionService.GetByIdAsync(id);

            if (p == null)
            {
                return NotFound();
            }

            await _promotionService.RemoveAsync(p);

            return NoContent();
        }
    }
}
