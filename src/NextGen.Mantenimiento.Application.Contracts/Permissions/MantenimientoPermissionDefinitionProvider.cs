using NextGen.Mantenimiento.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;
using Volo.Abp.MultiTenancy;

namespace NextGen.Mantenimiento.Permissions;

public class MantenimientoPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        //var personalGroup = context.AddGroup(MantenimientoPermissions.GroupName, L("Permission:Personal"));

        //var personalPermission = personalGroup.AddPermission(MantenimientoPermissions.Personal.Default, L("Permission:Personal"));
        //personalPermission.AddChild(MantenimientoPermissions.Personal.Create, L("Permission:Create"));
        //personalPermission.AddChild(MantenimientoPermissions.Personal.Edit, L("Permission:Edit"));
        //personalPermission.AddChild(MantenimientoPermissions.Personal.Delete, L("Permission:Delete"));


        //var departamentoGroup = context.AddGroup(MantenimientoPermissions.GroupName, L("Permission:Departamento"));

        //var departamentoPermission = departamentoGroup.AddPermission(MantenimientoPermissions.Departamento.Default, L("Permission:Departamento"));
        //departamentoPermission.AddChild(MantenimientoPermissions.Departamento.Create, L("Permission:Create"));
        //departamentoPermission.AddChild(MantenimientoPermissions.Departamento.Edit, L("Permission:Edit"));
        //departamentoPermission.AddChild(MantenimientoPermissions.Departamento.Delete, L("Permission:Delete"));
        //Define your own permissions here. Example:
        //myGroup.AddPermission(MantenimientoPermissions.MyPermission1, L("Permission:MyPermission1"));

        // Definir un solo grupo "Mantenimiento"
        var mantenimientoGroup = context.AddGroup(MantenimientoPermissions.GroupName, L("Permission:Mantenimiento"));

        // Definir permisos para "Personal"
        var personalPermission = mantenimientoGroup.AddPermission(MantenimientoPermissions.Personal.Default, L("Permission:Personal"));
        personalPermission.AddChild(MantenimientoPermissions.Personal.Create, L("Permission:Create"));
        personalPermission.AddChild(MantenimientoPermissions.Personal.Edit, L("Permission:Edit"));
        personalPermission.AddChild(MantenimientoPermissions.Personal.Delete, L("Permission:Delete"));

        // Definir permisos para "Departamento"
        var departamentoPermission = mantenimientoGroup.AddPermission(MantenimientoPermissions.Departamento.Default, L("Permission:Departamento"));
        departamentoPermission.AddChild(MantenimientoPermissions.Departamento.Create, L("Permission:Departamento.Create"));
        departamentoPermission.AddChild(MantenimientoPermissions.Departamento.Edit, L("Permission:Departamento.Edit"));
        departamentoPermission.AddChild(MantenimientoPermissions.Departamento.Delete, L("Permission:Departamento.Delete"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<MantenimientoResource>(name);
    }
}
