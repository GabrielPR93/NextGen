using NextGen.Mantenimiento.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;
using Volo.Abp.MultiTenancy;

namespace NextGen.Mantenimiento.Permissions;

public class MantenimientoPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var personalGroup = context.AddGroup(MantenimientoPermissions.GroupName, L("Permission:Personal"));

        var personalPermission = personalGroup.AddPermission(MantenimientoPermissions.Personal.Default, L("Permission:Personal"));
        personalPermission.AddChild(MantenimientoPermissions.Personal.Create, L("Permission:Create"));
        personalPermission.AddChild(MantenimientoPermissions.Personal.Edit, L("Permission:Edit"));
        personalPermission.AddChild(MantenimientoPermissions.Personal.Delete, L("Permission:Delete"));

        //Define your own permissions here. Example:
        //myGroup.AddPermission(MantenimientoPermissions.MyPermission1, L("Permission:MyPermission1"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<MantenimientoResource>(name);
    }
}
