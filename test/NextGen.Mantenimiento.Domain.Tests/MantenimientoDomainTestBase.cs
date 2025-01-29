using Volo.Abp.Modularity;

namespace NextGen.Mantenimiento;

/* Inherit from this class for your domain layer tests. */
public abstract class MantenimientoDomainTestBase<TStartupModule> : MantenimientoTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
