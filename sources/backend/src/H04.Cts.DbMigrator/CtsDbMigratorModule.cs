using H04.Cts.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace H04.Cts.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(CtsEntityFrameworkCoreModule),
    typeof(CtsApplicationContractsModule)
)]
public class CtsDbMigratorModule : AbpModule
{
}