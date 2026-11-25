using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShelterApi.Models;
using ZstdSharp.Unsafe;

namespace ShelterApi.Controllers
{
    [Route("api/queries")]
    [ApiController]
    public class QueriesController : ControllerBase
    {
        private readonly AnimalshelterContext _context;

        public QueriesController(AnimalshelterContext context)
        {
            _context = context;
        }

        [HttpGet("/queries/allSheltersWithAnimals")]
        public async Task<IActionResult> AllSheltersWithAnimals()
        {
            try
            {
                var shelters = await _context.Shelters.Include(s => s.Animals).ToListAsync();
                if (shelters is not null) return StatusCode(200, new { message = "Sikeres lekérdezés", result = shelters });
                else return StatusCode(404, new { message = "Sikertelen lekérdezés", result = shelters });
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }
    }
}
