using AMMAAPI.Models;
using AMMAAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace AMMAAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class EventsController : ControllerBase
    {
        private readonly EventsService _eventsService;
        public EventsController(EventsService eventsService) =>
            _eventsService = eventsService;

        [HttpGet]
        public async Task<ActionResult<List<Event>>> Get() =>
            await _eventsService.GetAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<Event>> Get(string id)
        {
            var e = await _eventsService.GetByIdAsync(id);

            if (e == null)
            {
                return NotFound();
            }

            return e;
        }

        [HttpPost]
        public async Task<IActionResult> Post(Event e)
        {
            e.EventId = ObjectId.GenerateNewId().ToString().Substring(0,24);

            await _eventsService.CreateAsync(e);

            return CreatedAtAction(nameof(Get), new { id = e.EventId }, e);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, Event eIn)
        {
            var e = _eventsService.GetByIdAsync;

            if (e == null)
            {
                return NotFound();
            }

            await _eventsService.UpdateAsync(eIn);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var e = await _eventsService.GetByIdAsync(id);

            if (e == null)
            {
                return NotFound();
            }

            await _eventsService.RemoveAsync(e);

            return Ok();
        }
        
    }
}
