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

        [HttpGet("/queries/allAnimalsWithShelter")]
        public async Task<IActionResult> AllAnimalsWithShelter()
        {
            try
            {
                var animals = await _context.Animals.Join(_context.Shelters, a => a.ShelterId, s => s.Id, (a, s) => new { a.Id, a.Name, a.Species, ShelterName = s.Name }).ToListAsync();
                if (animals is not null) return StatusCode(200, new { message = "Sikeres lekérdezés", result = animals });
                else return StatusCode(404, new { message = "Sikertelen lekérdezés", result = animals });
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpGet("/queries/animalsOfShelter")]
        public async Task<IActionResult> AnimalsOfShelter(Guid shelterId)
        {
            try
            {
                var shelter = await _context.Shelters.Include(s => s.Animals).Where(s => s.Id == shelterId).FirstOrDefaultAsync();
                if (shelter is not null) return StatusCode(200, new { message = "Sikeres lekérdezés", result = shelter });
                else return StatusCode(404, new { message = "Sikertelen lekérdezés", result = shelter });
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpGet("/queries/animalCountByShelter")]
        public async Task<IActionResult> AnimalCountByShelter()
        {
            try
            {
                var shelter = await _context.Shelters.Select(s => new{ s.Id, s.Name, AnimalCount = s.Animals.Count()}).OrderByDescending(s => s.AnimalCount).ToListAsync();
                if (shelter is not null) return StatusCode(200, new { message = "Sikeres lekérdezés", result = shelter });
                else return StatusCode(404, new { message = "Sikertelen lekérdezés", result = shelter });
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }
    }
}
