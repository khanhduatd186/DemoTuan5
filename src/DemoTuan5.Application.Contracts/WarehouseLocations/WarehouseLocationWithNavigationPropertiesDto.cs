using DemoTuan5.Countries;
using DemoTuan5.Warehouses;

using System;
using Volo.Abp.Application.Dtos;
using System.Collections.Generic;

namespace DemoTuan5.WarehouseLocations
{
    public abstract class WarehouseLocationWithNavigationPropertiesDtoBase
    {
        public WarehouseLocationDto WarehouseLocation { get; set; } = null!;

        public CountryDto Country { get; set; } = null!;
        public WarehouseDto Warehouse { get; set; } = null!;

    }
}