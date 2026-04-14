using API.DA.API.DA.Context.Scaffolded;
using API.SERVICE.Interfaces;
using Microsoft.EntityFrameworkCore;
using API.SERVICE.Entities;
using API.SERVICE.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.DA.Context;

public partial class AppDbContext : DbContext, IAppDbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }
    public virtual DbSet<CulturalSite> CulturalSites { get; set; }
    public virtual DbSet<CulturalSiteTag> CulturalSiteTags { get; set; }
    public virtual DbSet<Department> Departments { get; set; }
    public virtual DbSet<Locality> Localities { get; set; }
    public virtual DbSet<Province> Provinces { get; set; }
    public virtual DbSet<SiteContact> SiteContacts { get; set; }
    public virtual DbSet<SiteInfoBlock> SiteInfoBlocks { get; set; }
    public virtual DbSet<SiteLink> SiteLinks { get; set; }
    public virtual DbSet<SiteSchedule> SiteSchedules { get; set; }
    public virtual DbSet<Tag> Tags { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__categori__3213E83FF47059C8");

            entity.ToTable("categories");

            entity.HasIndex(e => e.Slug, "UQ__categori__32DD1E4CAF67BD19").IsUnique();

            entity.HasIndex(e => e.Name, "UQ__categori__72E12F1B8041F5AE").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Color)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("color");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("created_at");
            entity.Property(e => e.Description)
                .IsUnicode(false)
                .HasColumnName("description");
            entity.Property(e => e.Icon)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("icon");
            entity.Property(e => e.Name)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Slug)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("slug");
        });

        modelBuilder.Entity<CulturalSite>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__cultural__3213E83F0FD64655");

            entity.ToTable("cultural_sites");

            entity.HasIndex(e => e.CategoryId, "IX_cultural_sites_category_id");

            entity.HasIndex(e => e.DepartmentId, "IX_cultural_sites_department_id");

            entity.HasIndex(e => e.IsPublished, "IX_cultural_sites_is_published");

            entity.HasIndex(e => e.LocalityId, "IX_cultural_sites_locality_id");

            entity.HasIndex(e => e.ProvinceId, "IX_cultural_sites_province_id");

            entity.HasIndex(e => e.Slug, "UQ__cultural__32DD1E4CA29A3E92").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AddressLine)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("address_line");
            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("created_at");
            entity.Property(e => e.DepartmentId).HasColumnName("department_id");
            entity.Property(e => e.Description)
                .IsUnicode(false)
                .HasColumnName("description");
            entity.Property(e => e.EntryType)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("entry_type");
            entity.Property(e => e.ImageUrl)
                .IsUnicode(false)
                .HasColumnName("image_url");
            entity.Property(e => e.InstitutionName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("institution_name");
            entity.Property(e => e.IsFeatured).HasColumnName("is_featured");
            entity.Property(e => e.IsPublished)
                .HasDefaultValue(true)
                .HasColumnName("is_published");
            entity.Property(e => e.Latitude)
                 .HasPrecision(10, 7)
                 .HasColumnName("latitude");
            entity.Property(e => e.LocalityId).HasColumnName("locality_id");
            entity.Property(e => e.Longitude)
                 .HasPrecision(10, 7)
                 .HasColumnName("longitude");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.OwnershipType)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("ownership_type");
            entity.Property(e => e.ProvinceId).HasColumnName("province_id");
            entity.Property(e => e.ShortDescription)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("short_description");
            entity.Property(e => e.Slug)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("slug");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Category).WithMany(p => p.CulturalSites)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_cultural_sites_categories");

            entity.HasOne(d => d.Department).WithMany(p => p.CulturalSites)
                .HasForeignKey(d => d.DepartmentId)
                .HasConstraintName("FK_cultural_sites_departments");

            entity.HasOne(d => d.Locality).WithMany(p => p.CulturalSites)
                .HasForeignKey(d => d.LocalityId)
                .HasConstraintName("FK_cultural_sites_localities");

            entity.HasOne(d => d.Province).WithMany(p => p.CulturalSites)
                .HasForeignKey(d => d.ProvinceId)
                .HasConstraintName("FK_cultural_sites_provinces");
        });

        modelBuilder.Entity<CulturalSiteTag>(entity =>
        {
            entity.HasKey(e => new { e.CulturalSiteId, e.TagId });

            entity.ToTable("cultural_site_tags");

            entity.Property(e => e.CulturalSiteId).HasColumnName("cultural_site_id");
            entity.Property(e => e.TagId).HasColumnName("tag_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("created_at");

            entity.HasOne(d => d.CulturalSite).WithMany(p => p.CulturalSiteTags)
                .HasForeignKey(d => d.CulturalSiteId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_cultural_site_tags_site");

            entity.HasOne(d => d.Tag).WithMany(p => p.CulturalSiteTags)
                .HasForeignKey(d => d.TagId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_cultural_site_tags_tag");
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__departme__3213E83F3BC18F23");

            entity.ToTable("departments");

            entity.HasIndex(e => new { e.ProvinceId, e.Name }, "UQ_departments_province_name").IsUnique();

            entity.HasIndex(e => new { e.ProvinceId, e.Slug }, "UQ_departments_province_slug").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("created_at");
            entity.Property(e => e.Name)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.ProvinceId).HasColumnName("province_id");
            entity.Property(e => e.Slug)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("slug");

            entity.HasOne(d => d.Province).WithMany(p => p.Departments)
                .HasForeignKey(d => d.ProvinceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_departments_provinces");
        });

        modelBuilder.Entity<Locality>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__localiti__3213E83F5FA9A334");

            entity.ToTable("localities");

            entity.HasIndex(e => new { e.DepartmentId, e.Name }, "UQ_localities_department_name").IsUnique();

            entity.HasIndex(e => new { e.DepartmentId, e.Slug }, "UQ_localities_department_slug").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("created_at");
            entity.Property(e => e.DepartmentId).HasColumnName("department_id");
            entity.Property(e => e.Name)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.PostalCode)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("postal_code");
            entity.Property(e => e.Slug)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("slug");

            entity.HasOne(d => d.Department).WithMany(p => p.Localities)
                .HasForeignKey(d => d.DepartmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_localities_departments");
        });

        modelBuilder.Entity<Province>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__province__3213E83F289C32B3");

            entity.ToTable("provinces");

            entity.HasIndex(e => e.Slug, "UQ__province__32DD1E4C47BF5E3A").IsUnique();

            entity.HasIndex(e => e.Name, "UQ__province__72E12F1BD83117C1").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("created_at");
            entity.Property(e => e.Name)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Slug)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("slug");
        });

        modelBuilder.Entity<SiteContact>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__site_con__3213E83F50EFB7CE");

            entity.ToTable("site_contacts");

            entity.HasIndex(e => e.SiteId, "IX_site_contacts_site_id");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AddressLine)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("address_line");
            entity.Property(e => e.ContactType)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("contact_type");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("created_at");
            entity.Property(e => e.Email)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Label)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("label");
            entity.Property(e => e.Phone)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("phone");
            entity.Property(e => e.SiteId).HasColumnName("site_id");
            entity.Property(e => e.SortOrder).HasColumnName("sort_order");

            entity.HasOne(d => d.Site).WithMany(p => p.SiteContacts)
                .HasForeignKey(d => d.SiteId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_site_contacts_site");
        });

        modelBuilder.Entity<SiteInfoBlock>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__site_inf__3213E83FB3C24FAD");

            entity.ToTable("site_info_blocks");

            entity.HasIndex(e => e.SiteId, "IX_site_info_blocks_site_id");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.BlockType)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("block_type");
            entity.Property(e => e.Content)
                .IsUnicode(false)
                .HasColumnName("content");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("created_at");
            entity.Property(e => e.SiteId).HasColumnName("site_id");
            entity.Property(e => e.SortOrder).HasColumnName("sort_order");
            entity.Property(e => e.Title)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("title");

            entity.HasOne(d => d.Site).WithMany(p => p.SiteInfoBlocks)
                .HasForeignKey(d => d.SiteId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_site_info_blocks_site");
        });

        modelBuilder.Entity<SiteLink>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__site_lin__3213E83F75DDB2C1");

            entity.ToTable("site_links");

            entity.HasIndex(e => e.SiteId, "IX_site_links_site_id");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("created_at");
            entity.Property(e => e.Label)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("label");
            entity.Property(e => e.LinkType)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("link_type");
            entity.Property(e => e.SiteId).HasColumnName("site_id");
            entity.Property(e => e.SortOrder).HasColumnName("sort_order");
            entity.Property(e => e.Url)
                .IsUnicode(false)
                .HasColumnName("url");

            entity.HasOne(d => d.Site).WithMany(p => p.SiteLinks)
                .HasForeignKey(d => d.SiteId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_site_links_site");
        });

        modelBuilder.Entity<SiteSchedule>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__site_sch__3213E83F25AE5D99");

            entity.ToTable("site_schedules");

            entity.HasIndex(e => e.SiteId, "IX_site_schedules_site_id");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("created_at");
            entity.Property(e => e.Description)
                .IsUnicode(false)
                .HasColumnName("description");
            entity.Property(e => e.ScheduleType)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("schedule_type");
            entity.Property(e => e.SiteId).HasColumnName("site_id");
            entity.Property(e => e.SortOrder).HasColumnName("sort_order");
            entity.Property(e => e.Title)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("title");

            entity.HasOne(d => d.Site).WithMany(p => p.SiteSchedules)
                .HasForeignKey(d => d.SiteId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_site_schedules_site");
        });

        modelBuilder.Entity<Tag>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tags__3213E83F54C38212");

            entity.ToTable("tags");

            entity.HasIndex(e => e.Slug, "UQ__tags__32DD1E4C469B4D12").IsUnique();

            entity.HasIndex(e => e.Name, "UQ__tags__72E12F1B43275EFB").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("created_at");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Slug)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("slug");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}