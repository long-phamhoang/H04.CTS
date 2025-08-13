using H04.Cts.Entities.DanhMucs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace H04.Cts.EntityFrameworkCore.EntityConfigurations.DanhMucs;

public class DieuKienCapCTSTheoLLConfiguration : IEntityTypeConfiguration<DieuKienCapCTSTheoLL>
{
  public void Configure(EntityTypeBuilder<DieuKienCapCTSTheoLL> builder)
  {
    builder.ToTable(CtsConsts.DbTablePrefix + "DieuKienCapCTSTheoLLs", CtsConsts.DbSchema);
    builder.ConfigureByConvention();

    builder.HasIndex(x => new { x.MaDieuKien }).IsUnique().HasFilter(@"""IsDeleted"" = false");
    builder.HasIndex(x => new { x.TrangThai });

    builder.HasOne(x => x.LucLuong)
        .WithMany()
        .HasForeignKey(x => x.LucLuongId);
  }
}


