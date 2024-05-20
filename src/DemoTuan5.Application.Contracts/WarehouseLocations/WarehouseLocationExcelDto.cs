using System;

namespace DemoTuan5.WarehouseLocations
{
    public abstract class WarehouseLocationExcelDtoBase
    {
        public string? Code { get; set; }
        public string? Description { get; set; }
        public bool Active { get; set; }
        public int Idx { get; set; }
    }
}