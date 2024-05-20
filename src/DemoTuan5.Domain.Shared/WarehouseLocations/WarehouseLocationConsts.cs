namespace DemoTuan5.WarehouseLocations
{
    public static class WarehouseLocationConsts
    {
        private const string DefaultSorting = "{0}Code asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "WarehouseLocation." : string.Empty);
        }

    }
}