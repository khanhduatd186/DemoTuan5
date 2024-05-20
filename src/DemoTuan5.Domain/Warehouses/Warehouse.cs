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

namespace DemoTuan5.Warehouses
{
    public abstract class WarehouseBase : AuditedEntity<Guid>, IHasConcurrencyStamp
    {
        [CanBeNull]
        public virtual string? Code { get; set; }

        [CanBeNull]
        public virtual string? Description { get; set; }

        public virtual bool Active { get; set; }

        public string ConcurrencyStamp { get; set; }

        protected WarehouseBase()
        {

        }

        public WarehouseBase(Guid id, bool active, string? code = null, string? description = null)
        {
            ConcurrencyStamp = Guid.NewGuid().ToString("N");
            Id = id;
            Active = active;
            Code = code;
            Description = description;
        }

    }
}