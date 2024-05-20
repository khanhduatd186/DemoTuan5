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

namespace DemoTuan5.Countries
{
    public abstract class CountryBase : AuditedEntity<Guid>, IHasConcurrencyStamp
    {
        [NotNull]
        public virtual string Code { get; set; }

        [CanBeNull]
        public virtual string? Description { get; set; }

        public string ConcurrencyStamp { get; set; }

        protected CountryBase()
        {

        }

        public CountryBase(Guid id, string code, string? description = null)
        {
            ConcurrencyStamp = Guid.NewGuid().ToString("N");
            Id = id;
            Check.NotNull(code, nameof(code));
            Code = code;
            Description = description;
        }

    }
}