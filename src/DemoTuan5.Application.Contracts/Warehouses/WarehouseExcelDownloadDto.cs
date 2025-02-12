using Volo.Abp.Application.Dtos;
using System;

namespace DemoTuan5.Warehouses
{
    public abstract class WarehouseExcelDownloadDtoBase
    {
        public string DownloadToken { get; set; } = null!;

        public string? FilterText { get; set; }

        public string? Code { get; set; }
        public string? Description { get; set; }
        public bool? Active { get; set; }

        public WarehouseExcelDownloadDtoBase()
        {

        }
    }
}