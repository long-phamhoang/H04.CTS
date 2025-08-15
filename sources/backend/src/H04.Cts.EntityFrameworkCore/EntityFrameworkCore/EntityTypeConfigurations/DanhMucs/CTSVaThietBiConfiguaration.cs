using H04.Cts.DanhMucs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace H04.Cts.EntityFrameworkCore.EntityConfigurations.DanhMucs;

public class CtsVaThietBiConfiguration : IEntityTypeConfiguration<CtsVaThietBi>
{
    public void Configure(EntityTypeBuilder<CtsVaThietBi> builder)
    {
        builder.ToTable(CtsConsts.DbTablePrefix + "CtsVaThietBis", CtsConsts.DbSchema);
        builder.ConfigureByConvention();

        #region Index
        builder.HasIndex(x => new { x.SoHieuCts }).IsUnique().HasFilter(@"""IsDeleted"" = false");
        builder.HasIndex(x => new { x.TenCts, x.ToChucId, x.TrangThai });
        #endregion

        #region Collection

        #endregion
    }
}