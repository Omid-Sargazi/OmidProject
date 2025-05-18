using OmidProject.Domains.Domain.General;
using OmidProject.Domains.Domain.Identity;
using OmidProject.Domains.Domain.Project;
using OmidProject.Domains.Domain.SystemMessages;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace OmidProject.Infrastructures.CommandDb;

public class OmidProjectBaseCommandDb : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
{
    public OmidProjectBaseCommandDb(DbContextOptions<OmidProjectBaseCommandDb> options) : base(options)
    {
        ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("Persian_100_CI_AS");
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        base.OnModelCreating(modelBuilder);
    }

    public DbSet<Project> Projects { get; set; }
    public DbSet<SystemMessage> SystemMessages { get; set; }
    public DbSet<AccessibleForm> AccessibleForms { get; set; }
    public DbSet<RoleAccessibleForm> RoleAccessibleForms { get; set; }
    public DbSet<FrontPageForm> FrontPageForms { get; set; }
    public DbSet<RoleFrontPageForm> RoleFrontPageForms { get; set; }
    public DbSet<Document> Documents { get; set; }


    public DbSet<Province> Provinces { get; set; }
    public DbSet<City> Cities { get; set; }
    public DbSet<District> Districts { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Advertisement> Advertisements { get; set; }
    public DbSet<AdvertisementImage> AdvertisementImages { get; set; }
}