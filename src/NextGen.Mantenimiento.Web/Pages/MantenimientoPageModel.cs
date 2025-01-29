using NextGen.Mantenimiento.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace NextGen.Mantenimiento.Web.Pages;

public abstract class MantenimientoPageModel : AbpPageModel
{
    protected MantenimientoPageModel()
    {
        LocalizationResourceType = typeof(MantenimientoResource);
    }
}
