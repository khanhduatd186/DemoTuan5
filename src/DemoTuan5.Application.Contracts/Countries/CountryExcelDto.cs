using System;

namespace DemoTuan5.Countries
{
    public abstract class CountryExcelDtoBase
    {
        public string Code { get; set; } = null!;
        public string? Description { get; set; }
    }
}