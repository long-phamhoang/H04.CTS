using System;
using H04.Cts.Entities.DanhMucs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace H04.Cts.EntityFrameworkCore.EntityTypeConfigurations.DanhMucs;

public class MangCTSConfiguration : IEntityTypeConfiguration<MangCTS>
{
    public void Configure(EntityTypeBuilder<MangCTS> builder)
    {
        builder.ToTable(CtsConsts.DbTablePrefix + "MangHeThongCapCTSs", CtsConsts.DbSchema);
        builder.ConfigureByConvention();

        #region Index
        builder.HasIndex(x => new { x.MaMangCTS }).IsUnique().HasFilter(@"""IsDeleted"" = false");
        #endregion
    }
}
