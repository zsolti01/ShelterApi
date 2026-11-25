namespace ShelterApi.Models.DTOs
{
    public class UpdateAnimalDto
    {
        public string Name { get; set; } = null!;

        public string Species { get; set; } = null!;

        public int Age { get; set; }

        public double Weight { get; set; }
    }
}
