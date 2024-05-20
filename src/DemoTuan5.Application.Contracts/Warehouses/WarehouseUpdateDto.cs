using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Application.Dtos;

namespace DemoTuan5.Warehouses
{
    public abstract class WarehouseUpdateDtoBase : AuditedEntityDto<Guid>,IHasConcurrencyStamp
    {
        [Required]
        public string Code { get; set; } = null!;
        public string? Description { get; set; }
        public bool Active { get; set; }

        public string ConcurrencyStamp { get; set; } = null!;
    }
}