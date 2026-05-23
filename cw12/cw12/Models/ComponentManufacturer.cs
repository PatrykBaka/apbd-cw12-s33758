using System;
using System.Collections.Generic;

namespace cw12.Models;

public partial class ComponentManufacturer
{
    public int Id { get; set; }

    public string Abbrevation { get; set; } = null!;

    public string FullName { get; set; } = null!;

    public DateOnly FoundationDate { get; set; }

    public virtual ICollection<Component> Components { get; set; } = new List<Component>();
}
