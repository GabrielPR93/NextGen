using NextGen.Mantenimiento.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;
using Volo.Abp.MultiTenancy;

namespace NextGen.Mantenimiento.Permissions;

public class MantenimientoPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {

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

        // Definir permisos para "Categoria"
        var categoriaPermission = mantenimientoGroup.AddPermission(MantenimientoPermissions.Categoria.Default, L("Permission:Categoria"));
        categoriaPermission.AddChild(MantenimientoPermissions.Categoria.Create, L("Permission:Categoria.Create"));
        categoriaPermission.AddChild(MantenimientoPermissions.Categoria.Edit, L("Permission:Categoria.Edit"));
        categoriaPermission.AddChild(MantenimientoPermissions.Categoria.Delete, L("Permission:Categoria.Delete"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<MantenimientoResource>(name);
    }
}
