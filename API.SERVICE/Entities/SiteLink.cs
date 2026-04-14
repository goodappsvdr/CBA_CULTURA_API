using System;
using System.Collections.Generic;

namespace API.DA.API.DA.Context.Scaffolded;

public partial class SiteLink
{
    public int Id { get; set; }

    public int SiteId { get; set; }

    public string LinkType { get; set; } = null!;

    public string? Label { get; set; }

    public string Url { get; set; } = null!;

    public int SortOrder { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual CulturalSite Site { get; set; } = null!;
}
