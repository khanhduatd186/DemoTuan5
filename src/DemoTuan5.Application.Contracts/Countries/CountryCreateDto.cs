using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace DemoTuan5.Countries
{
    public abstract class CountryCreateDtoBase
    {
        public string? Code { get; set; }
        public string? Description { get; set; }
    }
}