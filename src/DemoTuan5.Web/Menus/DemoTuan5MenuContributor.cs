using DemoTuan5.Permissions;
using System.Threading.Tasks;
using Volo.Abp.UI.Navigation;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using DemoTuan5.Localization;
using Volo.Abp.Authorization.Permissions;

namespace DemoTuan5.Web.Menus;

public class DemoTuan5MenuContributor : IMenuContributor
{
    public async Task ConfigureMenuAsync(MenuConfigurationContext context)
    {
        if (context.Menu.Name != StandardMenus.Main)
        {
            return;
        }

        var moduleMenu = AddModuleMenuItem(context); //Do not delete `moduleMenu` variable as it will be used by ABP Suite!

        AddMenuItemCountries(context, moduleMenu);

        AddMenuItemWarehouses(context, moduleMenu);

        AddMenuItemWarehouseLocations(context, moduleMenu);
    }

    private static ApplicationMenuItem AddModuleMenuItem(MenuConfigurationContext context)
    {
        var l = context.GetLocalizer<DemoTuan5Resource>();

        var moduleMenu = new ApplicationMenuItem(
            DemoTuan5Menus.Prefix,
            displayName: l["Menu:DemoTuan5"],
            "~/DemoTuan5",
            icon: "fa fa-globe");

        //Add main menu items.
        context.Menu.Items.AddIfNotContains(moduleMenu);
        return moduleMenu;
    }
    private static void AddMenuItemCountries(MenuConfigurationContext context, ApplicationMenuItem parentMenu)
    {
        parentMenu.AddItem(
            new ApplicationMenuItem(
                Menus.DemoTuan5Menus.Countries,
                context.GetLocalizer<DemoTuan5Resource>()["Menu:Countries"],
                "/DemoTuan5/Countries",
                icon: "fa fa-file-alt",
                requiredPermissionName: DemoTuan5Permissions.Countries.Default
            )
        );
    }

    private static void AddMenuItemWarehouses(MenuConfigurationContext context, ApplicationMenuItem parentMenu)
    {
        parentMenu.AddItem(
            new ApplicationMenuItem(
                Menus.DemoTuan5Menus.Warehouses,
                context.GetLocalizer<DemoTuan5Resource>()["Menu:Warehouses"],
                "/DemoTuan5/Warehouses",
                icon: "fa fa-file-alt",
                requiredPermissionName: DemoTuan5Permissions.Warehouses.Default
            )
        );
    }

    private static void AddMenuItemWarehouseLocations(MenuConfigurationContext context, ApplicationMenuItem parentMenu)
    {
        parentMenu.AddItem(
            new ApplicationMenuItem(
                Menus.DemoTuan5Menus.WarehouseLocations,
                context.GetLocalizer<DemoTuan5Resource>()["Menu:WarehouseLocations"],
                "/DemoTuan5/WarehouseLocations",
                icon: "fa fa-file-alt",
                requiredPermissionName: DemoTuan5Permissions.WarehouseLocations.Default
            )
        );
    }
}