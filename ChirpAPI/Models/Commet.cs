using System;
using System.Collections;
using System.Collections.Generic;

namespace ChirpAPI.Models;

public partial class Commet
{
    public int Id { get; set; }

    public int ChirpId { get; set; }

    public int? ParentId { get; set; }

    public BitArray Text { get; set; } = null!;

    public DateTime CreationDate { get; set; }

    public virtual Chirp Chirp { get; set; } = null!;

    public virtual ICollection<Commet> InverseParent { get; set; } = new List<Commet>();

    public virtual Commet? Parent { get; set; }
}
