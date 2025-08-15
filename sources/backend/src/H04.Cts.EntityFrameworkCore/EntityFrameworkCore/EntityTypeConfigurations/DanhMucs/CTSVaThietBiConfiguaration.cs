using H04.Cts.DanhMucs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace H04.Cts.EntityFrameworkCore.EntityConfigurations.DanhMucs;

public class CTSVaThietBiConfiguration : IEntityTypeConfiguration<CTSVaThietBi>
{
    public void Configure(EntityTypeBuilder<CTSVaThietBi> builder)
    {
        builder.ToTable(CtsConsts.DbTablePrefix + "CTSVaThietBis", CtsConsts.DbSchema);
        builder.ConfigureByConvention();

        #region Index
        builder.HasIndex(x => new { x.SoHieuCTS }).IsUnique().HasFilter(@"""IsDeleted"" = false");
        builder.HasIndex(x => new { x.TenCTS, x.CoQuanToChuc, x.TrangThai });
        #endregion

        #region Collection

        #endregion
    }
}