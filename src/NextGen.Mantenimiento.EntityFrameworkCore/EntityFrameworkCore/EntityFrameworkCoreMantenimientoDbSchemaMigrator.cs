using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NextGen.Mantenimiento.Data;
using Volo.Abp.DependencyInjection;

namespace NextGen.Mantenimiento.EntityFrameworkCore;

public class EntityFrameworkCoreMantenimientoDbSchemaMigrator
    : IMantenimientoDbSchemaMigrator, ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public EntityFrameworkCoreMantenimientoDbSchemaMigrator(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        /* We intentionally resolving the MantenimientoDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<MantenimientoDbContext>()
            .Database
            .MigrateAsync();
    }
}
