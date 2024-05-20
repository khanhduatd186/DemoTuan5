using System;
using System.Collections.Generic;

using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace DemoTuan5.Warehouses
{
    public abstract class WarehouseDtoBase : AuditedEntityDto<Guid>, IHasConcurrencyStamp
    {
        public string Code { get; set; } = null!;
        public string? Description { get; set; }
        public bool Active { get; set; }

        public string ConcurrencyStamp { get; set; } = null!;

    }
}