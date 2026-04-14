using System;
using System.Collections.Generic;

namespace API.DA.API.DA.Context.Scaffolded;

public partial class Locality
{
    public int Id { get; set; }

    public int DepartmentId { get; set; }

    public string Name { get; set; } = null!;

    public string Slug { get; set; } = null!;

    public string? PostalCode { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<CulturalSite> CulturalSites { get; set; } = new List<CulturalSite>();

    public virtual Department Department { get; set; } = null!;
}
