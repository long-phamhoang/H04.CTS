using H04.Cts.Entities.DanhMucs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace H04.Cts.EntityFrameworkCore.EntityConfigurations.DanhMucs;

public class ToChucConfiguration : IEntityTypeConfiguration<ToChuc>
{
    public void Configure(EntityTypeBuilder<ToChuc> builder)
    {
        builder.ToTable(CtsConsts.DbTablePrefix + "ToChucs", CtsConsts.DbSchema);
        builder.ConfigureByConvention();

        #region Index
        builder.HasIndex(x => new { x.ToChucCapTrenId });
        builder.HasIndex(x => new { x.MaToChuc }).IsUnique().HasFilter(@"""IsDeleted"" = false");
        builder.HasIndex(x => new { x.MaSoThue });
        builder.HasIndex(x => new { x.DienThoai });
        builder.HasIndex(x => new { x.MaQuanHeNganSach });
        builder.HasIndex(x => new { x.CapCoQuanId });
        builder.HasIndex(x => new { x.TinhThanhPhoId });
        builder.HasIndex(x => new { x.XaPhuongId });
        builder.HasIndex(x => new { x.TrangThai });
        #endregion

        #region Collection
        builder.HasMany(x => x.ToChucCapDuois).WithOne(x => x.ToChucCapTrenFk).HasForeignKey(x => x.ToChucCapTrenId);
        builder.HasMany(x => x.ThueBaoCaNhans).WithOne(x => x.ToChucFk).HasForeignKey(x => x.ToChucId);
        // builder.HasMany(x => x.ThietBiDichVuPhanMems).WithOne(x => x.CoQuanChuQuanFk).HasForeignKey(x => x.CoQuanChuQuanId);
        // builder.HasMany(x => x.NguoiTiepNhans).WithOne(x => x.CoQuanNopHoSoFk).HasForeignKey(x => x.CoQuanNopHoSoId);
        #endregion
    }
}