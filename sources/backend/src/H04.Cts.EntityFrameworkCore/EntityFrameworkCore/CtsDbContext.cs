using H04.Cts.Entities.DanhMucs;
using H04.Cts.EntityFrameworkCore.EntityConfigurations.DanhMucs;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.BackgroundJobs.EntityFrameworkCore;
using Volo.Abp.BlobStoring.Database.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Identity;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.OpenIddict.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;

namespace H04.Cts.EntityFrameworkCore;

[ReplaceDbContext(typeof(IIdentityDbContext))]
[ConnectionStringName("Default")]
public class CtsDbContext :
    AbpDbContext<CtsDbContext>,
    IIdentityDbContext
{
    #region 0. Root
    /* Notice:
     * We only implemented IIdentityProDbContext and replaced them for this DbContext.
     * This allows you to perform JOIN queries for the entities of these modules over the repositories easily.
     * You typically don't need that for other modules.
     * But, if you need, you can implement the DbContext interface of the needed module and use ReplaceDbContext attribute just like IIdentityProDbContext.
     *
     * More info: Replacing a DbContext of a module ensures that the related module uses this DbContext on runtime. Otherwise, it will use its own DbContext class.
     */

    public DbSet<IdentityUser> Users { get; set; }
    public DbSet<IdentityRole> Roles { get; set; }
    public DbSet<IdentityClaimType> ClaimTypes { get; set; }
    public DbSet<OrganizationUnit> OrganizationUnits { get; set; }
    public DbSet<IdentitySecurityLog> SecurityLogs { get; set; }
    public DbSet<IdentityLinkUser> LinkUsers { get; set; }
    public DbSet<IdentityUserDelegation> UserDelegations { get; set; }
    public DbSet<IdentitySession> Sessions { get; set; }
    #endregion

    #region 1. DanhMucs
    public DbSet<ToChuc> ToChucs { get; set; }
    public DbSet<NoiCapCCCD> NoiCapCCCDs { get; set; }
    public DbSet<NguoiTiepNhan> NguoiTiepNhans { get; set; }
    #endregion

    public CtsDbContext(DbContextOptions<CtsDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        #region 0. Root
        builder.ConfigurePermissionManagement();
        builder.ConfigureSettingManagement();
        builder.ConfigureBackgroundJobs();
        builder.ConfigureAuditLogging();
        builder.ConfigureFeatureManagement();
        builder.ConfigureIdentity();
        builder.ConfigureOpenIddict();
        builder.ConfigureBlobStoring();
        #endregion

        #region 1. DanhMucs
        builder.ApplyConfiguration(new ToChucConfiguration());
        builder.ApplyConfiguration(new NoiCapCCCDConfiguration());
        builder.ApplyConfiguration(new NguoiTiepNhanConfiguration());
        #endregion
    }
}