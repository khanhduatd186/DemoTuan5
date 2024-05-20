using DemoTuan5.Shared;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using DemoTuan5.WarehouseLocations;
using Volo.Abp.Content;
using DemoTuan5.Shared;

namespace DemoTuan5.WarehouseLocations
{
    [RemoteService(Name = "DemoTuan5")]
    [Area("demoTuan5")]
    [ControllerName("WarehouseLocation")]
    [Route("api/demo-tuan5/warehouse-locations")]
    public abstract class WarehouseLocationControllerBase : AbpController
    {
        protected IWarehouseLocationsAppService _warehouseLocationsAppService;

        public WarehouseLocationControllerBase(IWarehouseLocationsAppService warehouseLocationsAppService)
        {
            _warehouseLocationsAppService = warehouseLocationsAppService;
        }

        [HttpGet]
        public virtual Task<PagedResultDto<WarehouseLocationWithNavigationPropertiesDto>> GetListAsync(GetWarehouseLocationsInput input)
        {
            return _warehouseLocationsAppService.GetListAsync(input);
        }

        [HttpGet]
        [Route("with-navigation-properties/{id}")]
        public virtual Task<WarehouseLocationWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            return _warehouseLocationsAppService.GetWithNavigationPropertiesAsync(id);
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<WarehouseLocationDto> GetAsync(Guid id)
        {
            return _warehouseLocationsAppService.GetAsync(id);
        }

        [HttpGet]
        [Route("country-lookup")]
        public virtual Task<PagedResultDto<LookupDto<Guid>>> GetCountryLookupAsync(LookupRequestDto input)
        {
            return _warehouseLocationsAppService.GetCountryLookupAsync(input);
        }

        [HttpGet]
        [Route("warehouse-lookup")]
        public virtual Task<PagedResultDto<LookupDto<Guid>>> GetWarehouseLookupAsync(LookupRequestDto input)
        {
            return _warehouseLocationsAppService.GetWarehouseLookupAsync(input);
        }

        [HttpPost]
        public virtual Task<WarehouseLocationDto> CreateAsync(WarehouseLocationCreateDto input)
        {
            return _warehouseLocationsAppService.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<WarehouseLocationDto> UpdateAsync(Guid id, WarehouseLocationUpdateDto input)
        {
            return _warehouseLocationsAppService.UpdateAsync(id, input);
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return _warehouseLocationsAppService.DeleteAsync(id);
        }

        [HttpGet]
        [Route("as-excel-file")]
        public virtual Task<IRemoteStreamContent> GetListAsExcelFileAsync(WarehouseLocationExcelDownloadDto input)
        {
            return _warehouseLocationsAppService.GetListAsExcelFileAsync(input);
        }

        [HttpGet]
        [Route("download-token")]
        public virtual Task<DemoTuan5.Shared.DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            return _warehouseLocationsAppService.GetDownloadTokenAsync();
        }
    }
}