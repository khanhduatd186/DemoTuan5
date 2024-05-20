using System;

namespace DemoTuan5.Warehouses
{
    public abstract class WarehouseExcelDtoBase
    {
        public string? Code { get; set; }
        public string? Description { get; set; }
        public bool Active { get; set; }
    }
}