using System;
using System.Collections.Generic;

namespace API.DA.API.DA.Context.Scaffolded;

public partial class Department
{
    public int Id { get; set; }

    public int ProvinceId { get; set; }

    public string Name { get; set; } = null!;

    public string Slug { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<CulturalSite> CulturalSites { get; set; } = new List<CulturalSite>();

    public virtual ICollection<Locality> Localities { get; set; } = new List<Locality>();

    public virtual Province Province { get; set; } = null!;
}
