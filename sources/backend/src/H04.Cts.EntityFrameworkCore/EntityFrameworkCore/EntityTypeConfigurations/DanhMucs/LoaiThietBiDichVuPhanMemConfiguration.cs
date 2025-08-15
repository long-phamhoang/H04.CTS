using H04.Cts.DanhMucs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace H04.Cts.EntityFrameworkCore.EntityTypeConfigurations.DanhMucs;
public class LoaiThietBiDichVuPhanMemConfiguration : IEntityTypeConfiguration<LoaiThietBiDichVuPhanMem>
{
    public void Configure(EntityTypeBuilder<LoaiThietBiDichVuPhanMem> builder)
    {
        builder.ToTable(CtsConsts.DbTablePrefix + "LoaiThietBiDichVuPhanMems", CtsConsts.DbSchema);
        builder.ConfigureByConvention();

        #region Index
        builder.HasIndex(x => new { x.TenLoaiThietBiDichVuPhanMem });
        builder.HasIndex(x => new { x.MaLoaiThietBiDichVuPhanMem }).IsUnique().HasFilter(@"""IsDeleted"" = false");
        builder.HasIndex(x => new { x.TrangThai });
        #endregion

        #region Collection
        builder.HasMany(x => x.ThietBiDichVuPhanMems).WithOne(x => x.LoaiThietBiDichVuPhanMemFk).HasForeignKey(x => x.LoaiThietBiDichVuPhanMemId);
        // builder.HasMany(x => x.ThueBaoCaNhans).WithOne(x => x.CoQuanChuQuanFk).HasForeignKey(x => x.CoQuanChuQuanId);
        // builder.HasMany(x => x.ThietBiDichVuPhanMems).WithOne(x => x.CoQuanChuQuanFk).HasForeignKey(x => x.CoQuanChuQuanId);
        // builder.HasMany(x => x.NguoiTiepNhans).WithOne(x => x.CoQuanNopHoSoFk).HasForeignKey(x => x.CoQuanNopHoSoId);
        #endregion
    }
}
