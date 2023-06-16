using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Zadanie7.DAL;
using Zadanie7.DTO;

namespace Zadanie7.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TripsController : ControllerBase
    {
        public readonly MasterContext _context;
        public TripsController(MasterContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult GetTrips()
        {
            return Ok(_context.Trips.Include(elem => elem.ClientTrips)
                                   .Include(elem => elem.Countries)
                                   .OrderByDescending(elem => elem.DateTo)
                                   .Select(elem => new
                                   {
                                       elem.Name,
                                       elem.Description,
                                       elem.DateFrom,
                                       elem.DateTo,
                                       elem.MaxPeople,
                                       Countries = elem.Countries.Select(elem => new
                                                                 {
                                                                     elem.Name
                                                                 }),
                                       Clients = elem.ClientTrips.Select(elem =>  new
                                                                 {
                                                                     elem.Clients.FirstName,
                                                                     elem.Clients.LastName 
                                                                 })
                                   })
                                   .ToList());
        }

        [HttpPost("{idTrip}/clients")]
        public async Task<IActionResult> CreateClientTrip(ClientTripDTO clientTripDTO)
        {
            var client = await _context.Clients.Include(elem => elem.ClientTrips)
                                                        .Where(elem => elem.Pesel == clientTripDTO.Pesel)
                                                       .FirstOrDefaultAsync();
            if (client == null)
            {
                client = new Client
                {
                    FirstName = clientTripDTO.FirstName,
                    LastName = clientTripDTO.LastName,
                    Email = clientTripDTO.Email,
                    Telephone = clientTripDTO.Telephone,
                    Pesel = clientTripDTO.Pesel
                };
                await _context.Clients.AddAsync(client);
                await _context.SaveChangesAsync();
            }
            
            var enrolledTripExists = await _context.ClientTrips
                .Where(elem => elem.IdTrip == clientTripDTO.IdTrip && elem.IdClient == client.IdClient)
                .FirstOrDefaultAsync() != null;

            if (enrolledTripExists)
            {
                return BadRequest(enrolledTripExists.ToString());
            }
            var trip = await _context.Trips
                .Where(elem => elem.IdTrip == clientTripDTO.IdTrip && elem.Name == clientTripDTO.TripName)
                .FirstOrDefaultAsync();
            if (trip == null)
            {
                return BadRequest("Such trip does not exist");
            }
            var clientTrip = new ClientTrip
            {
                IdClient = client.IdClient,
                IdTrip = trip.IdTrip,
                RegisteredAt = DateTime.Now,
                PaymentDate = clientTripDTO.PaymentDate
            };      
            await _context.ClientTrips.AddAsync(clientTrip);
            await _context.SaveChangesAsync();
            return Ok("Succesful creation");
        }
        
    }
}