using H04.Cts.Entities.DanhMucs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace H04.Cts.EntityFrameworkCore.EntityConfigurations.DanhMucs;

public class LucLuongConfiguration : IEntityTypeConfiguration<LucLuong>
{
    public void Configure(EntityTypeBuilder<LucLuong> builder)
    {
        builder.ToTable(CtsConsts.DbTablePrefix + "LucLuongs", CtsConsts.DbSchema);
        builder.ConfigureByConvention();

        #region Index
        builder.HasIndex(x => new { x.MaLucLuong }).IsUnique().HasFilter(@"""IsDeleted"" = false");
        builder.HasIndex(x => new { x.TrangThai });
        #endregion
    }
}
