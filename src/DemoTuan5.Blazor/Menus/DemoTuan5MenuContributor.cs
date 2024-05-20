using DemoTuan5.Permissions;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using DemoTuan5.Localization;
using Volo.Abp.UI.Navigation;

namespace DemoTuan5.Blazor.Menus;

public class DemoTuan5MenuContributor : IMenuContributor
{
    public async Task ConfigureMenuAsync(MenuConfigurationContext context)
    {
        if (context.Menu.Name == StandardMenus.Main)
        {
            await ConfigureMainMenuAsync(context);
        }

        var moduleMenu = AddModuleMenuItem(context);
        AddMenuItemCountries(context, moduleMenu);

        AddMenuItemWarehouses(context, moduleMenu);

    }

    private static async Task ConfigureMainMenuAsync(MenuConfigurationContext context)
    {
        //Add main menu items.
        var l = context.GetLocalizer<DemoTuan5Resource>();

        context.Menu.AddItem(new ApplicationMenuItem(
            DemoTuan5Menus.Prefix,
            displayName: l["Menu:DemoTuan5"],
            "/DemoTuan5",
            icon: "fa fa-globe"));

        await Task.CompletedTask;
    }
    private static ApplicationMenuItem AddModuleMenuItem(MenuConfigurationContext context)
    {
        var moduleMenu = new ApplicationMenuItem(
            DemoTuan5Menus.Prefix,
            context.GetLocalizer<DemoTuan5Resource>()["Menu:DemoTuan5"],
            icon: "fa fa-folder"
        );

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

}