using Volo.Abp.Application.Dtos;
using System;

namespace DemoTuan5.Countries
{
    public abstract class CountryExcelDownloadDtoBase
    {
        public string DownloadToken { get; set; } = null!;

        public string? FilterText { get; set; }

        public string? Code { get; set; }
        public string? Description { get; set; }

        public CountryExcelDownloadDtoBase()
        {

        }
    }
}