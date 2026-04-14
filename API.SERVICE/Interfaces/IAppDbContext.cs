using API.DA.API.DA.Context.Scaffolded;
using API.SERVICE.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace API.SERVICE.Interfaces;

public interface IAppDbContext
{
    DbSet<Category> Categories { get; }
    DbSet<CulturalSite> CulturalSites { get; }
    DbSet<CulturalSiteTag> CulturalSiteTags { get; }
    DbSet<Department> Departments { get; }
    DbSet<Locality> Localities { get; }
    DbSet<Province> Provinces { get; }
    DbSet<SiteContact> SiteContacts { get; }
    DbSet<SiteInfoBlock> SiteInfoBlocks { get; }
    DbSet<SiteLink> SiteLinks { get; }
    DbSet<SiteSchedule> SiteSchedules { get; }
    DbSet<Tag> Tags { get; }

    DatabaseFacade Database { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}