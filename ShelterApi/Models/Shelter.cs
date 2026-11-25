using System;
using System.Collections.Generic;

namespace ShelterApi.Models;

public partial class Shelter
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string City { get; set; } = null!;

    public int Capacity { get; set; }

    public DateTime? RegTime { get; set; }

    public virtual ICollection<Animal> Animals { get; set; } = new List<Animal>();
}
