using H04.Cts.Entities.DanhMucs;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace H04.Cts.EntityFrameworkCore.EntityConfigurations.DanhMucs;

public class NguoiTiepNhanConfiguration : IEntityTypeConfiguration<NguoiTiepNhan>
{
    public void Configure(EntityTypeBuilder<NguoiTiepNhan> builder)
    {
        builder.ToTable(CtsConsts.DbTablePrefix + "NguoiTiepNhans", CtsConsts.DbSchema);
        builder.ConfigureByConvention();

        #region Index
        builder.HasIndex(x => new { x.FullName });
        builder.HasIndex(x => new { x.CCCD });
        builder.HasIndex(x => new { x.DateOfIssue });
        builder.HasIndex(x => new { x.NoiCapCCCDId });
        builder.HasIndex(x => new { x.Position });
        builder.HasIndex(x => new { x.Phone });
        builder.HasIndex(x => new { x.Email });
        #endregion

        builder
            .HasOne(r => r.NoiCapCCCDFk)
            .WithMany()
            .HasForeignKey(r => r.NoiCapCCCDId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasMany(x => x.Organizations)
            .WithMany(x => x.NguoiTiepNhans)
            .UsingEntity<Dictionary<string, object>>(
                "AppOrganizationNguoiTiepNhan",
                j => j
                    .HasOne<ToChuc>()
                    .WithMany()
                    .HasForeignKey("OrganizationId")
                    .HasConstraintName("FK_AppOrganizationNguoiTiepNhan_ToChuc_OrganizationId")
                    .OnDelete(DeleteBehavior.Cascade),
                j => j
                    .HasOne<NguoiTiepNhan>()
                    .WithMany()
                    .HasForeignKey("NguoiTiepNhanId")
                    .HasConstraintName("FK_AppOrganizationNguoiTiepNhan_NguoiTiepNhan_NguoiTiepNhanId")
                    .OnDelete(DeleteBehavior.Cascade),
                j =>
                {
                    j.ToTable(CtsConsts.DbTablePrefix + "OrganizationNguoiTiepNhans", CtsConsts.DbSchema);
                    j.HasKey("OrganizationId", "NguoiTiepNhanId");
                    j.HasIndex("OrganizationId");
                    j.HasIndex("NguoiTiepNhanId");
                }
            );
    }
}