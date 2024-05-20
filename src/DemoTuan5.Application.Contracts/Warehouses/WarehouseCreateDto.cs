using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace DemoTuan5.Warehouses
{
    public abstract class WarehouseCreateDtoBase
    {
        [Required]
        public string Code { get; set; } = null!;
        public string? Description { get; set; }
        public bool Active { get; set; } = false;
    }
}