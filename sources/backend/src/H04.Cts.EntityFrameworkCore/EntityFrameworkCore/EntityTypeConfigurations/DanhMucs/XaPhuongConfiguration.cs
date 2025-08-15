using H04.Cts.DanhMucs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace H04.Cts.EntityFrameworkCore.EntityTypeConfigurations.DanhMucs
{
    public class XaPhuongConfiguration : IEntityTypeConfiguration<XaPhuong>
    {
        public void Configure(EntityTypeBuilder<XaPhuong> builder)
        {
            builder.ToTable(CtsConsts.DbTablePrefix + "XaPhuongs", CtsConsts.DbSchema);
            builder.ConfigureByConvention();

            #region Thuộc tính
            builder.Property(x => x.MaXaPhuong)
                   .IsRequired(false) 
                   .HasMaxLength(16);

            builder.Property(x => x.TenXaPhuong)
                   .IsRequired()
                   .HasMaxLength(128);

            builder.Property(x => x.GhiChu)
                   .HasMaxLength(512);
            #endregion

            #region Index
            builder.HasIndex(x => x.MaXaPhuong);
            builder.HasIndex(x => x.TenXaPhuong);
            builder.HasIndex(x => x.TrangThai);
            builder.HasIndex(x => x.TinhThanhPhoId);
            #endregion

            #region Quan hệ
            builder.HasOne(x => x.TinhThanhPho)
                   .WithMany(t => t.XaPhuongs)
                   .HasForeignKey(x => x.TinhThanhPhoId)
                   .OnDelete(DeleteBehavior.Cascade);
            #endregion
        }
    }
}
