using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace DemoTuan5.WarehouseLocations
{
    public abstract class WarehouseLocationCreateDtoBase
    {
        public string? Code { get; set; }
        public string? Description { get; set; }
        public bool Active { get; set; }
        public int Idx { get; set; }
        public Guid? CountryId { get; set; }
        public Guid? WarehouseId { get; set; }
    }
}