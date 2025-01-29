using Volo.Abp.Modularity;

namespace NextGen.Mantenimiento;

[DependsOn(
    typeof(MantenimientoDomainModule),
    typeof(MantenimientoTestBaseModule)
)]
public class MantenimientoDomainTestModule : AbpModule
{

}
