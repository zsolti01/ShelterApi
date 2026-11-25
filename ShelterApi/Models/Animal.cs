using System;
using System.Collections.Generic;

namespace ShelterApi.Models;

public partial class Animal
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string Species { get; set; } = null!;

    public int Age { get; set; }

    public double Weight { get; set; }

    public DateTime? RegTime { get; set; }

    public Guid? ShelterId { get; set; }

    public virtual Shelter? Shelter { get; set; }
}
