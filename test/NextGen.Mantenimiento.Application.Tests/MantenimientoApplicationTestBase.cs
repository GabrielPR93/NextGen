using Volo.Abp.Modularity;

namespace NextGen.Mantenimiento;

public abstract class MantenimientoApplicationTestBase<TStartupModule> : MantenimientoTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
