using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Application.Dtos;

namespace DemoTuan5.WarehouseLocations
{
    public abstract class WarehouseLocationUpdateDtoBase : AuditedEntityDto<Guid>,IHasConcurrencyStamp
    {
        [Required]
        public string Code { get; set; } = null!;
        public string? Description { get; set; }
        public bool Active { get; set; }
        public int Idx { get; set; }
        public Guid? CountryId { get; set; }
        public Guid? WarehouseId { get; set; }

        public string ConcurrencyStamp { get; set; } = null!;
    }
}