﻿using System;
using System.Collections.Generic;

namespace ChirpAPI.Models;

public partial class Chirp
{
    public int Id { get; set; }

    public string Text { get; set; } = null!;

    public string? ExtUrl { get; set; }

    public DateTime CreationTime { get; set; }

    public double? Lat { get; set; }

    public double? Lng { get; set; }

    public virtual ICollection<Commet> Commets { get; set; } = new List<Commet>();
}
