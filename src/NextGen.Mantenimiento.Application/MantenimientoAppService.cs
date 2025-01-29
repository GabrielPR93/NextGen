using NextGen.Mantenimiento.Localization;
using Volo.Abp.Application.Services;

namespace NextGen.Mantenimiento;

/* Inherit your application services from this class.
 */
public abstract class MantenimientoAppService : ApplicationService
{
    protected MantenimientoAppService()
    {
        LocalizationResource = typeof(MantenimientoResource);
    }
}
