using H04.Cts.DanhMucs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace H04.Cts.EntityFrameworkCore.EntityTypeConfigurations.DanhMucs
{
    public class ThueBaoCaNhanConfiguration : IEntityTypeConfiguration<ThueBaoCaNhan>
    {
        public void Configure(EntityTypeBuilder<ThueBaoCaNhan> builder)
        {
            builder.ToTable(CtsConsts.DbTablePrefix + "ThueBaoCaNhans", CtsConsts.DbSchema);
            builder.ConfigureByConvention();

            #region Index
            builder.HasIndex(x => x.ToChucId);
            builder.HasIndex(x => x.ChucVuId);
            builder.HasIndex(x => x.SoDinhDanhCaNhan).IsUnique();
            builder.HasIndex(x => x.TinhThanhPho);
            builder.HasIndex(x => x.PhuongXa);
            #endregion

            #region Collection
            builder.HasOne(x => x.ToChucFk).WithMany(x => x.ThueBaoCaNhans).HasForeignKey(x => x.ToChucId);

            builder.HasOne(x => x.ChucVuFk).WithMany(x => x.ThueBaoCaNhans).HasForeignKey(x => x.ChucVuId);
            #endregion
        }
    }

}
