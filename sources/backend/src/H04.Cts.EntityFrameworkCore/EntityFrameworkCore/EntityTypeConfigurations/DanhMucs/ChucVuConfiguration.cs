using H04.Cts.Entities.DanhMucs;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using H04.Cts.DanhMucs;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace H04.Cts.EntityFrameworkCore.EntityTypeConfigurations.DanhMucs
{
    public class ChucVuConfiguration : IEntityTypeConfiguration<ChucVu>
    {
        public void Configure(EntityTypeBuilder<ChucVu> builder)
        {
            builder.ToTable(CtsConsts.DbTablePrefix + "ChucVus", CtsConsts.DbSchema);
            builder.ConfigureByConvention();

            // Index
            builder.HasIndex(x => x.TenChucVu);
            builder.HasIndex(x => x.MaChucVu).IsUnique().HasFilter(@"""IsDeleted"" = false");
            builder.HasIndex(x => x.TrangThai);

            // Collection
            builder.HasMany(x => x.ThueBaoCaNhans).WithOne(x => x.ChucVuFk).HasForeignKey(x => x.ChucVuId);
        }
    }

}
