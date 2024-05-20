using DemoTuan5.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace DemoTuan5.Permissions;

public class DemoTuan5PermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(DemoTuan5Permissions.GroupName, L("Permission:DemoTuan5"));

        var countryPermission = myGroup.AddPermission(DemoTuan5Permissions.Countries.Default, L("Permission:Countries"));
        countryPermission.AddChild(DemoTuan5Permissions.Countries.Create, L("Permission:Create"));
        countryPermission.AddChild(DemoTuan5Permissions.Countries.Edit, L("Permission:Edit"));
        countryPermission.AddChild(DemoTuan5Permissions.Countries.Delete, L("Permission:Delete"));

        var warehousePermission = myGroup.AddPermission(DemoTuan5Permissions.Warehouses.Default, L("Permission:Warehouses"));
        warehousePermission.AddChild(DemoTuan5Permissions.Warehouses.Create, L("Permission:Create"));
        warehousePermission.AddChild(DemoTuan5Permissions.Warehouses.Edit, L("Permission:Edit"));
        warehousePermission.AddChild(DemoTuan5Permissions.Warehouses.Delete, L("Permission:Delete"));

        var warehouseLocationPermission = myGroup.AddPermission(DemoTuan5Permissions.WarehouseLocations.Default, L("Permission:WarehouseLocations"));
        warehouseLocationPermission.AddChild(DemoTuan5Permissions.WarehouseLocations.Create, L("Permission:Create"));
        warehouseLocationPermission.AddChild(DemoTuan5Permissions.WarehouseLocations.Edit, L("Permission:Edit"));
        warehouseLocationPermission.AddChild(DemoTuan5Permissions.WarehouseLocations.Delete, L("Permission:Delete"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<DemoTuan5Resource>(name);
    }
}