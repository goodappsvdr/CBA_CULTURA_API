using System;
using System.Collections.Generic;

namespace API.DA.API.DA.Context.Scaffolded;

public partial class SiteSchedule
{
    public int Id { get; set; }

    public int SiteId { get; set; }

    public string ScheduleType { get; set; } = null!;

    public string? Title { get; set; }

    public string Description { get; set; } = null!;

    public int SortOrder { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual CulturalSite Site { get; set; } = null!;
}
