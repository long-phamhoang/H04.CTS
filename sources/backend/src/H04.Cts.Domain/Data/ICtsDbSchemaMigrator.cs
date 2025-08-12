using System.Threading.Tasks;

namespace H04.Cts.Data;

public interface ICtsDbSchemaMigrator
{
    Task MigrateAsync();
}