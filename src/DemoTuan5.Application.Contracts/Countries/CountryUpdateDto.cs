using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;

namespace DemoTuan5.Countries
{
    public abstract class CountryUpdateDtoBase : IHasConcurrencyStamp
    {
        public string? Code { get; set; }
        public string? Description { get; set; }

        public string ConcurrencyStamp { get; set; } = null!;
    }
}