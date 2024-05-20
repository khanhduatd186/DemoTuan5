namespace DemoTuan5.Warehouses
{
    public static class WarehouseConsts
    {
        private const string DefaultSorting = "{0}Code asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "Warehouse." : string.Empty);
        }

    }
}