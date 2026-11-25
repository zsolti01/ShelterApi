using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShelterApi.Models;
using ShelterApi.Models.DTOs;
using Microsoft.EntityFrameworkCore;

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

        [HttpPost("/animals/newAnimal")]
        public async Task<object> NewAnimal([FromBody] NewAnimalDto newAnimalDto)
        {
            try
            {
                var animal = new Animal
                {
                    Id = Guid.NewGuid(),
                    Name = newAnimalDto.Name,
                    Species = newAnimalDto.Species,
                    Age = newAnimalDto.Age,
                    Weight = newAnimalDto.Weight,
                    RegTime = DateTime.Now
                };
                if (animal is not null)
                {
                    await _context.Animals.AddAsync(animal);
                    await _context.SaveChangesAsync();
                    return StatusCode(201, new { message = "Sikeres felvétel!", result = animal });
                }
                return StatusCode(400, new { message = "Sikertelen felvétel!", result = animal });
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpGet("/animals/allAnimal")]
        public async Task<IActionResult> GetAllAnimal()
        {
            try
            {
                var animal = await _context.Animals.ToListAsync();
                if (animal is not null) return StatusCode(200, new { message = "Sikeres lekérdezés", result = animal });
                else return StatusCode(404, new { message = "Sikertelen lekérdezés", result = animal });
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpGet("/animals/byId")]
        public async Task<IActionResult> GetById([FromQuery] Guid Id)
        {
            try
            {
                var animal = await _context.Animals.SingleOrDefaultAsync(a => a.Id == Id);
                if (animal != null) return StatusCode(200, new { message = "Sikeres lekérdezés", result = animal });
                return StatusCode(404, new { message = "Sikertelen lekérdezés", result = animal });
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpPut("/animals/update")]
        public async Task<IActionResult> Update([FromQuery] Guid id, UpdateAnimalDto updateAnimalDto)
        {
            try
            {
                var animal = await _context.Animals.FirstOrDefaultAsync(a => a.Id == id);
                if (animal is not null)
                {
                    animal.Name = updateAnimalDto.Name;
                    animal.Species = updateAnimalDto.Species;
                    animal.Age = updateAnimalDto.Age;
                    animal.Weight = updateAnimalDto.Weight;
                    _context.Animals.Update(animal);
                    await _context.SaveChangesAsync();
                    return StatusCode(200, new { message = "Sikeres frissítés", result = animal });
                }
                return StatusCode(404, new { message = "Sikertelen frissítés", result = animal });
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpDelete("/animals/deleteAnimal")]
        public async Task<IActionResult> DeleteAnimal([FromQuery] Guid id)
        {
            try
            {
                var animal = await _context.Animals.FindAsync(id);
                if (animal is not null)
                {
                    _context.Animals.Remove(animal);
                    await _context.SaveChangesAsync();
                    return StatusCode(200, new { message = "Sikeres törlés!", result = animal });
                }
                return StatusCode(404, new { message = "Sikertelen törlés!", result = animal });
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }
    }
}
