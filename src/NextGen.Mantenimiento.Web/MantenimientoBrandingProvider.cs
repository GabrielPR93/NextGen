using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;
using Microsoft.Extensions.Localization;
using NextGen.Mantenimiento.Localization;

namespace NextGen.Mantenimiento.Web;

[Dependency(ReplaceServices = true)]
public class MantenimientoBrandingProvider : DefaultBrandingProvider
{
    private IStringLocalizer<MantenimientoResource> _localizer;

    public MantenimientoBrandingProvider(IStringLocalizer<MantenimientoResource> localizer)
    {
        _localizer = localizer;
    }

    public override string AppName => _localizer["AppName"];
}
