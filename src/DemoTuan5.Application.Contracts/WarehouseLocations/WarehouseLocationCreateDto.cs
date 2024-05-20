using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace DemoTuan5.WarehouseLocations
{
    public abstract class WarehouseLocationCreateDtoBase
    {
        [Required]
        public string Code { get; set; } = null!;
        public string? Description { get; set; }
        public bool Active { get; set; }
        public int Idx { get; set; }
        public Guid? CountryId { get; set; }
        public Guid? WarehouseId { get; set; }
    }
}