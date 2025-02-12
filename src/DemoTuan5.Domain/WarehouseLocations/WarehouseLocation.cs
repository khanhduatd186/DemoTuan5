using DemoTuan5.Countries;
using DemoTuan5.Warehouses;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;
using Volo.Abp.Domain.Entities;

using Volo.Abp;

namespace DemoTuan5.WarehouseLocations
{
    public abstract class WarehouseLocationBase : AuditedEntity<Guid>, IHasConcurrencyStamp
    {
        [CanBeNull]
        public virtual string? Code { get; set; }

        [CanBeNull]
        public virtual string? Description { get; set; }

        public virtual bool Active { get; set; }

        public virtual int Idx { get; set; }
        public Guid? CountryId { get; set; }
        public Guid? WarehouseId { get; set; }

        public string ConcurrencyStamp { get; set; }

        protected WarehouseLocationBase()
        {

        }

        public WarehouseLocationBase(Guid id, Guid? countryId, Guid? warehouseId, bool active, int idx, string? code = null, string? description = null)
        {
            ConcurrencyStamp = Guid.NewGuid().ToString("N");
            Id = id;
            Active = active;
            Idx = idx;
            Code = code;
            Description = description;
            CountryId = countryId;
            WarehouseId = warehouseId;
        }

    }
}