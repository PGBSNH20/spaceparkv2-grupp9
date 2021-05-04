using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SpacePark_API.Models;
using SpacePark_API.Models.APIModels;


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
            return await _context.Parking.ToListAsync();

            //return await _context.Parking.Where(p => p.Paid == false).ToListAsync();
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

        // GET: api/Parking/boba fett/current
        [HttpGet("{name}/current")]
        public async Task<ActionResult<Parking>> GetCurrentParking(string name)
        {
            string nameWithSpace = ParkingValidation.GetWhiteSpaceName(name);
            var parking = await _context.Parking.Where(p => p.PersonName == nameWithSpace && p.Paid == false).FirstOrDefaultAsync();

            if (parking == null)
            {
                return NotFound("You have no current parking");
            }

            return parking;
        }

        // GET: api/boba fett/all
        [HttpGet("{name}/all")]
        public async Task<ActionResult<IEnumerable<Parking>>> GetAllParkingForCharacter(string name)
        {
            string nameWithSpace = ParkingValidation.GetWhiteSpaceName(name);
            return await _context.Parking.Where(p => p.PersonName == nameWithSpace).ToListAsync();
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

            if (DBMethods.AlreadyPaid(parking.ID))
            {
                return BadRequest("You have already paid");
            }

            try
            {
                var currentParking = _context.Parking
                .Where(p => p.ID == id)
                .FirstOrDefault();
                currentParking.Paid = true;

                TimeSpan timedPark = DateTime.Now - currentParking.ArrivalTime;
                var totalPrice = Math.Round(timedPark.TotalHours * 100, 2);

                var receipt = new Receipt { PayID = currentParking.ID, PersonName = currentParking.PersonName, StarShip = currentParking.StarShip, Price = totalPrice };
                _context.Receipts.Add(receipt);

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
        [HttpPost("register")]
        public async Task<ActionResult<Parking>> PostParking(ParkingRequest parkingRequest)
        {
            if (DBMethods.EmptySpaces(parkingRequest.SpacePortID))
            {
                var acceptableName = await ParkingValidation.ValidateName(parkingRequest.PersonName);
                var acceptableStarShip = await ParkingValidation.ValidateStarShip(parkingRequest.StarShip);

                if (acceptableName && acceptableStarShip)
                {
                    var spacePort = _context.SpacePorts.Find(parkingRequest.SpacePortID);
                    Parking newParking = new() { PersonName = parkingRequest.PersonName, StarShip = parkingRequest.StarShip, ArrivalTime = parkingRequest.ArrivalTime, SpacePort = spacePort };
                    _context.Parking.Add(newParking);
                    await _context.SaveChangesAsync();

                    return CreatedAtAction("GetParking", new { id = newParking.ID }, newParking);
                }
                return BadRequest("You are either not a Star Wars character or have the wrong ship, please leave.");
            }

            return BadRequest("SpacePort is full.");
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
