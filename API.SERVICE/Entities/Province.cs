using System;
using System.Collections.Generic;

namespace API.DA.API.DA.Context.Scaffolded;

public partial class Province
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Slug { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<CulturalSite> CulturalSites { get; set; } = new List<CulturalSite>();

    public virtual ICollection<Department> Departments { get; set; } = new List<Department>();
}
