using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using H04.Cts.DanhMucs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace H04.Cts.EntityFrameworkCore.EntityTypeConfigurations.DanhMucs
{
    public class LoaiHoSoConfiguration : IEntityTypeConfiguration<LoaiHoSo>
    {
        public void Configure(EntityTypeBuilder<LoaiHoSo> builder)
        {
            builder.ToTable(CtsConsts.DbTablePrefix + "LoaiHoSos", CtsConsts.DbSchema);
            builder.ConfigureByConvention();

            #region Index
            builder.HasIndex(x => new { x.MaLoaiHoSo }).IsUnique().HasFilter(@"""IsDeleted"" = false");
            builder.HasIndex(x => new { x.TrangThai });
            #endregion

            #region Collection
            #endregion
        }
    }
}
