using System;
using System.Collections.Generic;

namespace API.DA.API.DA.Context.Scaffolded;

public partial class Tag
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Slug { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<CulturalSiteTag> CulturalSiteTags { get; set; } = new List<CulturalSiteTag>();
}
