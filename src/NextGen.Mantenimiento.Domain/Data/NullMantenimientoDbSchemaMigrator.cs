using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace NextGen.Mantenimiento.Data;

/* This is used if database provider does't define
 * IMantenimientoDbSchemaMigrator implementation.
 */
public class NullMantenimientoDbSchemaMigrator : IMantenimientoDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
