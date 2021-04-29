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
    public class ParkingController : ControllerBase
    {
        private readonly MyContext _context;

        public ParkingController(MyContext context)
        {
            _context = context;
        }

        // GET: api/Parking
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Parking>>> GetParking()
        {
            //return await _context.Parking.ToListAsync();

            return await _context.Parking.Where(p => p.Payed == false).ToListAsync();
        }

        // GET: api/Parking/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Parking>> GetParking(int id)
        {
            var parking = await _context.Parking.FindAsync(id);

            if (parking == null)
            {
                return NotFound();
            }

            return parking;
        }

        // PUT: api/Parking/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutParking(int id, Parking parking)
        {
            if (id != parking.ID)
            {
                return BadRequest();
            }

            _context.Entry(parking).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ParkingExists(id))
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

        // POST: api/Parking
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Parking>> PostParking(Parking parking)
        {
            // var park = new Parking { PersonName = "Han solo", SpaceShip = "death star", ArrivalTime = DateTime.Now };

            _context.Parking.Add(parking);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetParking", new { id = parking.ID }, parking);
        }

        // DELETE: api/Parking/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteParking(int id)
        {
            var parking = await _context.Parking.FindAsync(id);
            if (parking == null)
            {
                return NotFound();
            }

            _context.Parking.Remove(parking);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ParkingExists(int id)
        {
            return _context.Parking.Any(e => e.ID == id);
        }
    }
}
