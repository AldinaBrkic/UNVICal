using Microsoft.AspNetCore.Mvc;
using UNVICal.Data;
using UNVICal.Models;

namespace UNVICal.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventsController : ControllerBase
    {
        private readonly EventsDbContext _context;

        public EventsController(EventsDbContext context)
        {
            _context = context;
        }

        // POST api/events
        [HttpPost]
        public async Task<IActionResult> AddEvent([FromBody] Event newEvent)
        {
            if (newEvent == null) return BadRequest();

            _context.Events.Add(newEvent);
            await _context.SaveChangesAsync();

            return Ok(newEvent);
        }

        // GET api/events
        [HttpGet]
        public IActionResult GetEvents()
        {
            return Ok(_context.Events.ToList());
        }

        // DELETE api/events/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            var ev = await _context.Events.FindAsync(id);
            if (ev == null)
            {
                return NotFound();
            }

            _context.Events.Remove(ev);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
