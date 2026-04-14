using System;
using System.Collections.Generic;

namespace API.DA.API.DA.Context.Scaffolded;

public partial class Category
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Slug { get; set; } = null!;

    public string? Description { get; set; }

    public string? Icon { get; set; }

    public string? Color { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<CulturalSite> CulturalSites { get; set; } = new List<CulturalSite>();
}
