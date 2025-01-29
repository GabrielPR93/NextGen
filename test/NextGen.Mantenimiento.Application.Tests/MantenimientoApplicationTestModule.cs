using Volo.Abp.Modularity;

namespace NextGen.Mantenimiento;

[DependsOn(
    typeof(MantenimientoApplicationModule),
    typeof(MantenimientoDomainTestModule)
)]
public class MantenimientoApplicationTestModule : AbpModule
{

}
