using System;
using System.Collections.Generic;

namespace API.DA.API.DA.Context.Scaffolded;

public partial class SiteContact
{
    public int Id { get; set; }

    public int SiteId { get; set; }

    public string ContactType { get; set; } = null!;

    public string? Label { get; set; }

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public string? AddressLine { get; set; }

    public int SortOrder { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual CulturalSite Site { get; set; } = null!;
}
