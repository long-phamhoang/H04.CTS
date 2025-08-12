using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace H04.Cts.Data;

/* This is used if database provider does't define
 * ICtsDbSchemaMigrator implementation.
 */
public class NullCtsDbSchemaMigrator : ICtsDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}