using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using DemoTuan5.Warehouses;
using Volo.Abp.Content;
using DemoTuan5.Shared;

namespace DemoTuan5.Warehouses
{
    [RemoteService(Name = "DemoTuan5")]
    [Area("demoTuan5")]
    [ControllerName("Warehouse")]
    [Route("api/demo-tuan5/warehouses")]
    public abstract class WarehouseControllerBase : AbpController
    {
        protected IWarehousesAppService _warehousesAppService;

        public WarehouseControllerBase(IWarehousesAppService warehousesAppService)
        {
            _warehousesAppService = warehousesAppService;
        }

        [HttpGet]
        public virtual Task<PagedResultDto<WarehouseDto>> GetListAsync(GetWarehousesInput input)
        {
            return _warehousesAppService.GetListAsync(input);
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<WarehouseDto> GetAsync(Guid id)
        {
            return _warehousesAppService.GetAsync(id);
        }

        [HttpPost]
        public virtual Task<WarehouseDto> CreateAsync(WarehouseCreateDto input)
        {
            return _warehousesAppService.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<WarehouseDto> UpdateAsync(Guid id, WarehouseUpdateDto input)
        {
            return _warehousesAppService.UpdateAsync(id, input);
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return _warehousesAppService.DeleteAsync(id);
        }

        [HttpGet]
        [Route("as-excel-file")]
        public virtual Task<IRemoteStreamContent> GetListAsExcelFileAsync(WarehouseExcelDownloadDto input)
        {
            return _warehousesAppService.GetListAsExcelFileAsync(input);
        }

        [HttpGet]
        [Route("download-token")]
        public virtual Task<DemoTuan5.Shared.DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            return _warehousesAppService.GetDownloadTokenAsync();
        }
    }
}