using System;
using System.Collections.Generic;

using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace DemoTuan5.WarehouseLocations
{
    public abstract class WarehouseLocationDtoBase : AuditedEntityDto<Guid>, IHasConcurrencyStamp
    {
        public string Code { get; set; } = null!;
        public string? Description { get; set; }
        public bool Active { get; set; }
        public int Idx { get; set; }
        public Guid? CountryId { get; set; }
        public Guid? WarehouseId { get; set; }

        public string ConcurrencyStamp { get; set; } = null!;

    }
}