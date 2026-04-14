using System;
using System.Collections.Generic;

namespace API.DA.API.DA.Context.Scaffolded;

public partial class CulturalSite
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Slug { get; set; } = null!;

    public string? ShortDescription { get; set; }

    public string? Description { get; set; }

    public string? AddressLine { get; set; }

    public decimal? Latitude { get; set; }

    public decimal? Longitude { get; set; }

    public int? ProvinceId { get; set; }

    public int? DepartmentId { get; set; }

    public int? LocalityId { get; set; }

    public int CategoryId { get; set; }

    public string? InstitutionName { get; set; }

    public string? OwnershipType { get; set; }

    public string? EntryType { get; set; }

    public string? ImageUrl { get; set; }

    public bool IsPublished { get; set; }

    public bool IsFeatured { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual Category Category { get; set; } = null!;

    public virtual ICollection<CulturalSiteTag> CulturalSiteTags { get; set; } = new List<CulturalSiteTag>();

    public virtual Department? Department { get; set; }

    public virtual Locality? Locality { get; set; }

    public virtual Province? Province { get; set; }

    public virtual ICollection<SiteContact> SiteContacts { get; set; } = new List<SiteContact>();

    public virtual ICollection<SiteInfoBlock> SiteInfoBlocks { get; set; } = new List<SiteInfoBlock>();

    public virtual ICollection<SiteLink> SiteLinks { get; set; } = new List<SiteLink>();

    public virtual ICollection<SiteSchedule> SiteSchedules { get; set; } = new List<SiteSchedule>();
}
