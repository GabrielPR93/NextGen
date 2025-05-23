﻿using System.Threading.Tasks;
using NextGen.Mantenimiento.Localization;
using NextGen.Mantenimiento.Permissions;
using NextGen.Mantenimiento.MultiTenancy;
using Volo.Abp.SettingManagement.Web.Navigation;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Identity.Web.Navigation;
using Volo.Abp.UI.Navigation;
using Volo.Abp.TenantManagement.Web.Navigation;
using System;

namespace NextGen.Mantenimiento.Web.Menus;

public class MantenimientoMenuContributor : IMenuContributor
{
    public async Task ConfigureMenuAsync(MenuConfigurationContext context)
    {
        if (context.Menu.Name == StandardMenus.Main)
        {
            await ConfigureMainMenuAsync(context);
        }
    }

    private static Task ConfigureMainMenuAsync(MenuConfigurationContext context)
    {
        var l = context.GetLocalizer<MantenimientoResource>();

        //Home
        context.Menu.AddItem(
            new ApplicationMenuItem(
                MantenimientoMenus.Home,
                l["Menu:Home"],
                "~/",
                icon: "fa fa-home",
                order: 1
            )
        );

        //Personal

        context.Menu.AddItem(
            new ApplicationMenuItem(
            "Personal",
            l["Menu:Personal"],
            icon: "fa-solid fa-people-group"
        ).AddItem(
            new ApplicationMenuItem(
            "Personal.empleados",
            l["Menu:Empleados"],
            icon: "fa-solid fa-user-tie",
            url: "/Personal"
        ).RequirePermissions(MantenimientoPermissions.Personal.Default)
    )
);

        //GestionCorporativa
        context.Menu.AddItem(
          new ApplicationMenuItem(
              "GestionCorporativa",
              l["Menu:GestionCorporativa"],
              icon: "fa-solid fa-building"
          ).AddItem(
              new ApplicationMenuItem(
                  "Departamento",
                  l["Menu:Departamentos"],
                  icon: "fa-solid fa-building",
                  url: "/Departamentos"
              ).RequirePermissions(MantenimientoPermissions.Departamento.Default)
          ).AddItem(
              new ApplicationMenuItem(
                  "Categoria",
                  l["Menu:Categorias"],
                  icon: "fa-solid fa-tags",
                  url: "/Categoria"
              ).RequirePermissions(MantenimientoPermissions.Categoria.Default)
          ).AddItem(
              new ApplicationMenuItem(
                  "Empresa",
                  l["Menu:Empresas"],
                  icon: "fa-solid fa-building",
                  url: "/Empresa"
              ).RequirePermissions(MantenimientoPermissions.Empresa.Default)
          )
      );

        //Checking
        context.Menu.AddItem(
       new ApplicationMenuItem(
           "Checking",
           l["Menu:Checking"],
           icon: "fa-solid fa-clock",
           url: "/Checking"
       ).RequirePermissions(MantenimientoPermissions.Checking.Default)
   );
        //TipoAcreditaciones
        context.Menu.AddItem(
            new ApplicationMenuItem(
                "TipoAcreditaciones",
                l["Menu:TipoAcreditaciones"],
                icon: "fa-solid fa-id-card",
                url: "/TipoAcreditaciones"
            ).RequirePermissions(MantenimientoPermissions.TipoAcreditaciones.Default)
        );

        //Administration
        var administration = context.Menu.GetAdministration();
        administration.Order = 5;

        //Administration->Identity
        administration.SetSubItemOrder(IdentityMenuNames.GroupName, 1);

        if (MultiTenancyConsts.IsEnabled)
        {
            administration.SetSubItemOrder(TenantManagementMenuNames.GroupName, 1);
        }
        else
        {
            administration.TryRemoveMenuItem(TenantManagementMenuNames.GroupName);
        }

        administration.SetSubItemOrder(SettingManagementMenuNames.GroupName, 3);

        //Administration->Settings
        administration.SetSubItemOrder(SettingManagementMenuNames.GroupName, 7);

        return Task.CompletedTask;
    }
}
