using H04.Cts.DanhMucs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace H04.Cts.EntityFrameworkCore.EntityTypeConfigurations.DanhMucs;
public class ThietBiDichVuPhanMemConfiguration : IEntityTypeConfiguration<ThietBiDichVuPhanMem>
{
    public void Configure(EntityTypeBuilder<ThietBiDichVuPhanMem> builder)
    {
        builder.ToTable(CtsConsts.DbTablePrefix + "ThietBiDichVuPhanMems", CtsConsts.DbSchema);
        builder.ConfigureByConvention();

        #region Index
        builder.HasIndex(x => new { x.TenCoQuanToChuc });
        builder.HasIndex(x => new { x.TenThietBiDichVuPhanMem });
        builder.HasIndex(x => new { x.LoaiThietBiDichVuPhanMemId });

        builder.HasIndex(x => new { x.DiaChiThuCongVu });
        builder.HasIndex(x => new { x.DiaChiIP });
        builder.HasIndex(x => new { x.DNS });
        builder.HasIndex(x => new { x.DiaChiIP1 });
        builder.HasIndex(x => new { x.DNS1 });

        builder.HasIndex(x => new { x.TinhThanhPhoId });
        #endregion

        #region Collection
        //builder.HasOne(x => x.LoaiThietBiDichVuPhanMem).WithMany(x => x.ToChucCapTrenFk).HasForeignKey(x => x.ToChucCapTrenId);
        // builder.HasMany(x => x.ThueBaoCaNhans).WithOne(x => x.CoQuanChuQuanFk).HasForeignKey(x => x.CoQuanChuQuanId);
        // builder.HasMany(x => x.ThietBiDichVuPhanMems).WithOne(x => x.CoQuanChuQuanFk).HasForeignKey(x => x.CoQuanChuQuanId);
        // builder.HasMany(x => x.NguoiTiepNhans).WithOne(x => x.CoQuanNopHoSoFk).HasForeignKey(x => x.CoQuanNopHoSoId);
        #endregion
    }
}
