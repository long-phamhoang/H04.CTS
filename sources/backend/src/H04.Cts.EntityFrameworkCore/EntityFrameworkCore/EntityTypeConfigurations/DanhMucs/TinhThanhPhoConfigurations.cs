using H04.Cts.DanhMucs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace H04.Cts.EntityFrameworkCore.EntityTypeConfigurations.DanhMucs
{
    public class TinhThanhPhoConfiguration : IEntityTypeConfiguration<TinhThanhPho>
    {
        public void Configure(EntityTypeBuilder<TinhThanhPho> builder)
        {
            builder.ToTable(CtsConsts.DbTablePrefix + "TinhThanhPhos", CtsConsts.DbSchema);
            builder.ConfigureByConvention();

            #region Thuộc tính
            builder.Property(x => x.MaTinhThanhPho)
                   .IsRequired(false)
                   .HasMaxLength(16);

            builder.Property(x => x.TenTinhThanhPho)
                   .IsRequired()
                   .HasMaxLength(128);

            builder.Property(x => x.GhiChu)
                   .HasMaxLength(512);
            #endregion

            #region Index
            builder.HasIndex(x => x.MaTinhThanhPho);
            builder.HasIndex(x => x.TenTinhThanhPho);
            builder.HasIndex(x => x.TrangThai);
            #endregion

            #region Quan hệ
            builder.HasMany(x => x.XaPhuongs)
                   .WithOne(xp => xp.TinhThanhPho)
                   .HasForeignKey(xp => xp.TinhThanhPhoId)
                   .OnDelete(DeleteBehavior.Cascade);
            #endregion
        }
    }
}
