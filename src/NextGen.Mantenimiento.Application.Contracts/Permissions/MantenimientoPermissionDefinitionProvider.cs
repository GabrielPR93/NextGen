using NextGen.Mantenimiento.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;
using Volo.Abp.MultiTenancy;

namespace NextGen.Mantenimiento.Permissions;

public class MantenimientoPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(MantenimientoPermissions.GroupName);

        //Define your own permissions here. Example:
        //myGroup.AddPermission(MantenimientoPermissions.MyPermission1, L("Permission:MyPermission1"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<MantenimientoResource>(name);
    }
}
