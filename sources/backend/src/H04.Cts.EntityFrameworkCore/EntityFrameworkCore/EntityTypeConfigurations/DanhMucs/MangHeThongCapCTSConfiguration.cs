using System;
using H04.Cts.Entities.DanhMucs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace H04.Cts.EntityFrameworkCore.EntityTypeConfigurations.DanhMucs;

public class MangHeThongCapCTSConfiguration : IEntityTypeConfiguration<MangHeThongCapCTS>
{
    public void Configure(EntityTypeBuilder<MangHeThongCapCTS> builder)
    {
        builder.ToTable(CtsConsts.DbTablePrefix + "MangHeThongCapCTSs", CtsConsts.DbSchema);
        builder.ConfigureByConvention();

        #region Index
        builder.HasIndex(x => new { x.MaMangHeThongCapCTS }).IsUnique().HasFilter(@"""IsDeleted"" = false");
        #endregion
    }
}
