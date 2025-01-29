using NextGen.Mantenimiento.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace NextGen.Mantenimiento.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(MantenimientoEntityFrameworkCoreModule),
    typeof(MantenimientoApplicationContractsModule)
)]
public class MantenimientoDbMigratorModule : AbpModule
{
}
