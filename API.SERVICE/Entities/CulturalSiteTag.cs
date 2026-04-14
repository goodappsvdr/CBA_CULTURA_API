using System;
using System.Collections.Generic;

namespace API.DA.API.DA.Context.Scaffolded;

public partial class CulturalSiteTag
{
    public int CulturalSiteId { get; set; }

    public int TagId { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual CulturalSite CulturalSite { get; set; } = null!;

    public virtual Tag Tag { get; set; } = null!;
}
