using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShelterApi.Models;

namespace ShelterApi.Controllers
{
    [Route("api/animals")]
    [ApiController]
    public class AnimalsController : ControllerBase
    {
        private readonly AnimalshelterContext _context;

        public AnimalsController(AnimalshelterContext context)
        {
            _context = context;
        }

        [HttpGet("/animals/allAnimal")]
        public object getAllAnimal()
        {
            try
            {
                var animal = _context.Animals.ToList();
                if (animal is not null) return StatusCode(200, new { message = "Sikeres lekérdezés", result = animal });
                else return StatusCode(400, new { message = "Sikertelen lekérdezés", result = animal });
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpGet("/animals/byId")]
        public object getById([FromQuery] Guid Id)
        {
            try
            {
                var animal = _context.Animals.SingleOrDefault(a => a.Id == Id);
                if (animal != null) return StatusCode(200, new { message = "Sikeres lekérdezés", result = animal });
                return StatusCode(404, new { message = "Sikertelen lekérdezés", result = animal });
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }
    }
}
