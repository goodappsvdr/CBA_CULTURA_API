using System;
using System.Collections.Generic;

namespace API.DA.API.DA.Context.Scaffolded;

public partial class SiteInfoBlock
{
    public int Id { get; set; }

    public int SiteId { get; set; }

    public string BlockType { get; set; } = null!;

    public string Title { get; set; } = null!;

    public string Content { get; set; } = null!;

    public int SortOrder { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual CulturalSite Site { get; set; } = null!;
}
