using DemoTuan5.Countries;
using DemoTuan5.Warehouses;

using System;
using System.Collections.Generic;

namespace DemoTuan5.WarehouseLocations
{
    public abstract class WarehouseLocationWithNavigationPropertiesBase
    {
        public WarehouseLocation WarehouseLocation { get; set; } = null!;

        public Country Country { get; set; } = null!;
        public Warehouse Warehouse { get; set; } = null!;
        

        
    }
}