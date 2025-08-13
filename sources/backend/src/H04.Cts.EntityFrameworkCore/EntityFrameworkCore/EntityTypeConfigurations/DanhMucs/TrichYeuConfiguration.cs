using H04.Cts.Entities.DanhMucs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace H04.Cts.EntityFrameworkCore.EntityConfigurations.DanhMucs;

public class TrichYeuConfiguration : IEntityTypeConfiguration<TrichYeu>
{
    public void Configure(EntityTypeBuilder<TrichYeu> builder)
    {
        builder.ToTable(CtsConsts.DbTablePrefix + "TrichYeus", CtsConsts.DbSchema);
        builder.ConfigureByConvention();

        #region Index
        builder.HasIndex(x => new { x.MaTrichYeu }).IsUnique().HasFilter(@"""IsDeleted"" = false");
        #endregion
    }
}