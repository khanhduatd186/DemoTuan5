using Volo.Abp.Reflection;

namespace DemoTuan5.Permissions;

public class DemoTuan5Permissions
{
    public const string GroupName = "DemoTuan5";

    public static string[] GetAll()
    {
        return ReflectionHelper.GetPublicConstantsRecursively(typeof(DemoTuan5Permissions));
    }

    public static class Countries
    {
        public const string Default = GroupName + ".Countries";
        public const string Edit = Default + ".Edit";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }

    public static class Warehouses
    {
        public const string Default = GroupName + ".Warehouses";
        public const string Edit = Default + ".Edit";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }

    public static class WarehouseLocations
    {
        public const string Default = GroupName + ".WarehouseLocations";
        public const string Edit = Default + ".Edit";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }
}