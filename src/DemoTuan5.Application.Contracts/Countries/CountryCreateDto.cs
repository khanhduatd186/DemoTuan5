using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace DemoTuan5.Countries
{
    public abstract class CountryCreateDtoBase
    {
        [Required]
        public string Code { get; set; } = null!;
        public string? Description { get; set; }
    }
}