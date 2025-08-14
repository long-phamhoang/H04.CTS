using H04.Cts.Entities.DanhMucs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace H04.Cts.EntityFrameworkCore.EntityConfigurations.DanhMucs;

public class NoiCapCCCDConfiguration : IEntityTypeConfiguration<NoiCapCCCD>
{
    public void Configure(EntityTypeBuilder<NoiCapCCCD> builder)
    {
        builder.ToTable(CtsConsts.DbTablePrefix + "NoiCapCCCDs", CtsConsts.DbSchema);
        builder.ConfigureByConvention();

        #region Index
        builder.HasIndex(x => new { x.Name });
        builder.HasIndex(x => new { x.Code });
        builder.HasIndex(x => new { x.Abbreviation });
        builder.HasIndex(x => new { x.Address });
        builder.HasIndex(x => new { x.Province });
        builder.HasIndex(x => new { x.IsActive });
        #endregion
    }
}