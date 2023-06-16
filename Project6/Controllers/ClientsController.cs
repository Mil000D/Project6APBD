using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Zadanie7.DAL;

namespace Zadanie7.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        public readonly MasterContext _context;
        public ClientsController(MasterContext context)
        {
            _context = context;
        }

        [HttpDelete("{idClient}")]
        public async Task<IActionResult> DeleteClient(int idClient)
        {
            var client = await _context.Clients.Include(elem => elem.ClientTrips)
                                                       .Where(elem => elem.IdClient == idClient && elem.ClientTrips.Count == 0)
                                                       .FirstOrDefaultAsync();
            if (client == null)
            {
                return BadRequest("Cannot delete client from database");
            }
            _context.Clients.Remove(client);
            await _context.SaveChangesAsync();
            return Ok($"Deleted client with id: {idClient}");
        }
    }
}
