using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SpacePark_API.Models;

namespace SpacePark_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpacePortsController : ControllerBase
    {
        public readonly MyContext _context;

        public SpacePortsController(MyContext context)
        {
            _context = context;
        }

        // GET: api/SpacePorts
        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<SpacePort>>> GetSpacePorts()
        {
            return await _context.SpacePorts.ToListAsync();
        }

        // GET: api/Spaceports/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SpacePort>> GetSpacePort(int id)
        {
            var spacePort = await _context.SpacePorts.FindAsync(id);

            if (spacePort == null)
            {
                return NotFound();
            }

            return spacePort;
        }

        // PUT: api/SpacePorts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSpacePort(int id, SpacePort spacePort)
        {
            if (id != spacePort.ID)
            {
                return BadRequest();
            }

            _context.Entry(spacePort).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SpacePortExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/SpacePorts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SpacePort>> PostSpacePort(SpacePort spacePort)
        {
            _context.SpacePorts.Add(spacePort);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSpacePort", new { id = spacePort.ID }, spacePort);
        }

        // DELETE: api/SpacePorts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSpacePort(int id)
        {
            var spacePort = await _context.SpacePorts.FindAsync(id);
            if (spacePort == null)
            {
                return NotFound("Are you soure you have the right id or because the cops maby already have found this spaceport");
            }

            _context.SpacePorts.Remove(spacePort);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SpacePortExists(int id)
        {
            return _context.SpacePorts.Any(e => e.ID == id);
        }
    }
}
