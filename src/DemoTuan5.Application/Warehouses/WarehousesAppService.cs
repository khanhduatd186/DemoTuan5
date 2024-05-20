using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using DemoTuan5.Permissions;
using DemoTuan5.Warehouses;
using MiniExcelLibs;
using Volo.Abp.Content;
using Volo.Abp.Authorization;
using Volo.Abp.Caching;
using Microsoft.Extensions.Caching.Distributed;
using DemoTuan5.Shared;

namespace DemoTuan5.Warehouses
{

    [Authorize(DemoTuan5Permissions.Warehouses.Default)]
    public abstract class WarehousesAppServiceBase : ApplicationService
    {
        protected IDistributedCache<WarehouseExcelDownloadTokenCacheItem, string> _excelDownloadTokenCache;
        protected IWarehouseRepository _warehouseRepository;
        protected WarehouseManager _warehouseManager;

        public WarehousesAppServiceBase(IWarehouseRepository warehouseRepository, WarehouseManager warehouseManager, IDistributedCache<WarehouseExcelDownloadTokenCacheItem, string> excelDownloadTokenCache)
        {
            _excelDownloadTokenCache = excelDownloadTokenCache;
            _warehouseRepository = warehouseRepository;
            _warehouseManager = warehouseManager;
        }

        public virtual async Task<PagedResultDto<WarehouseDto>> GetListAsync(GetWarehousesInput input)
        {
            var totalCount = await _warehouseRepository.GetCountAsync(input.FilterText, input.Code, input.Description, input.Active);
            var items = await _warehouseRepository.GetListAsync(input.FilterText, input.Code, input.Description, input.Active, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<WarehouseDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<Warehouse>, List<WarehouseDto>>(items)
            };
        }

        public virtual async Task<WarehouseDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<Warehouse, WarehouseDto>(await _warehouseRepository.GetAsync(id));
        }

        [Authorize(DemoTuan5Permissions.Warehouses.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _warehouseRepository.DeleteAsync(id);
        }

        [Authorize(DemoTuan5Permissions.Warehouses.Create)]
        public virtual async Task<WarehouseDto> CreateAsync(WarehouseCreateDto input)
        {

            var warehouse = await _warehouseManager.CreateAsync(
            input.Active, input.Code, input.Description
            );

            return ObjectMapper.Map<Warehouse, WarehouseDto>(warehouse);
        }

        [Authorize(DemoTuan5Permissions.Warehouses.Edit)]
        public virtual async Task<WarehouseDto> UpdateAsync(Guid id, WarehouseUpdateDto input)
        {

            var warehouse = await _warehouseManager.UpdateAsync(
            id,
            input.Active, input.Code, input.Description, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<Warehouse, WarehouseDto>(warehouse);
        }

        [AllowAnonymous]
        public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(WarehouseExcelDownloadDto input)
        {
            var downloadToken = await _excelDownloadTokenCache.GetAsync(input.DownloadToken);
            if (downloadToken == null || input.DownloadToken != downloadToken.Token)
            {
                throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
            }

            var items = await _warehouseRepository.GetListAsync(input.FilterText, input.Code, input.Description, input.Active);

            var memoryStream = new MemoryStream();
            await memoryStream.SaveAsAsync(ObjectMapper.Map<List<Warehouse>, List<WarehouseExcelDto>>(items));
            memoryStream.Seek(0, SeekOrigin.Begin);

            return new RemoteStreamContent(memoryStream, "Warehouses.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        public virtual async Task<DemoTuan5.Shared.DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await _excelDownloadTokenCache.SetAsync(
                token,
                new WarehouseExcelDownloadTokenCacheItem { Token = token },
                new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30)
                });

            return new DemoTuan5.Shared.DownloadTokenResultDto
            {
                Token = token
            };
        }
    }
}