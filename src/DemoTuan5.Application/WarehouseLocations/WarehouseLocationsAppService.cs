using DemoTuan5.Warehouses;
using DemoTuan5.Countries;
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
using DemoTuan5.WarehouseLocations;
using MiniExcelLibs;
using Volo.Abp.Content;
using Volo.Abp.Authorization;
using Volo.Abp.Caching;
using Microsoft.Extensions.Caching.Distributed;
using DemoTuan5.Shared;

namespace DemoTuan5.WarehouseLocations
{

    [Authorize(DemoTuan5Permissions.WarehouseLocations.Default)]
    public abstract class WarehouseLocationsAppServiceBase : ApplicationService
    {
        protected IDistributedCache<WarehouseLocationExcelDownloadTokenCacheItem, string> _excelDownloadTokenCache;
        protected IWarehouseLocationRepository _warehouseLocationRepository;
        protected WarehouseLocationManager _warehouseLocationManager;
        protected IRepository<Country, Guid> _countryRepository;
        protected IRepository<Warehouse, Guid> _warehouseRepository;

        public WarehouseLocationsAppServiceBase(IWarehouseLocationRepository warehouseLocationRepository, WarehouseLocationManager warehouseLocationManager, IDistributedCache<WarehouseLocationExcelDownloadTokenCacheItem, string> excelDownloadTokenCache, IRepository<Country, Guid> countryRepository, IRepository<Warehouse, Guid> warehouseRepository)
        {
            _excelDownloadTokenCache = excelDownloadTokenCache;
            _warehouseLocationRepository = warehouseLocationRepository;
            _warehouseLocationManager = warehouseLocationManager; _countryRepository = countryRepository;
            _warehouseRepository = warehouseRepository;
        }

        public virtual async Task<PagedResultDto<WarehouseLocationWithNavigationPropertiesDto>> GetListAsync(GetWarehouseLocationsInput input)
        {
            var totalCount = await _warehouseLocationRepository.GetCountAsync(input.FilterText, input.Code, input.Description, input.Active, input.IdxMin, input.IdxMax, input.CountryId, input.WarehouseId);
            var items = await _warehouseLocationRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.Code, input.Description, input.Active, input.IdxMin, input.IdxMax, input.CountryId, input.WarehouseId, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<WarehouseLocationWithNavigationPropertiesDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<WarehouseLocationWithNavigationProperties>, List<WarehouseLocationWithNavigationPropertiesDto>>(items)
            };
        }

        public virtual async Task<WarehouseLocationWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            return ObjectMapper.Map<WarehouseLocationWithNavigationProperties, WarehouseLocationWithNavigationPropertiesDto>
                (await _warehouseLocationRepository.GetWithNavigationPropertiesAsync(id));
        }

        public virtual async Task<WarehouseLocationDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<WarehouseLocation, WarehouseLocationDto>(await _warehouseLocationRepository.GetAsync(id));
        }

        public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetCountryLookupAsync(LookupRequestDto input)
        {
            var query = (await _countryRepository.GetQueryableAsync())
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                    x => x.Code != null &&
                         x.Code.Contains(input.Filter));

            var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<Country>();
            var totalCount = query.Count();
            return new PagedResultDto<LookupDto<Guid>>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<Country>, List<LookupDto<Guid>>>(lookupData)
            };
        }

        public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetWarehouseLookupAsync(LookupRequestDto input)
        {
            var query = (await _warehouseRepository.GetQueryableAsync())
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                    x => x.Code != null &&
                         x.Code.Contains(input.Filter));

            var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<Warehouse>();
            var totalCount = query.Count();
            return new PagedResultDto<LookupDto<Guid>>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<Warehouse>, List<LookupDto<Guid>>>(lookupData)
            };
        }

        [Authorize(DemoTuan5Permissions.WarehouseLocations.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _warehouseLocationRepository.DeleteAsync(id);
        }

        [Authorize(DemoTuan5Permissions.WarehouseLocations.Create)]
        public virtual async Task<WarehouseLocationDto> CreateAsync(WarehouseLocationCreateDto input)
        {

            var warehouseLocation = await _warehouseLocationManager.CreateAsync(
            input.CountryId, input.WarehouseId, input.Code, input.Active, input.Idx, input.Description
            );

            return ObjectMapper.Map<WarehouseLocation, WarehouseLocationDto>(warehouseLocation);
        }

        [Authorize(DemoTuan5Permissions.WarehouseLocations.Edit)]
        public virtual async Task<WarehouseLocationDto> UpdateAsync(Guid id, WarehouseLocationUpdateDto input)
        {

            var warehouseLocation = await _warehouseLocationManager.UpdateAsync(
            id,
            input.CountryId, input.WarehouseId, input.Code, input.Active, input.Idx, input.Description, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<WarehouseLocation, WarehouseLocationDto>(warehouseLocation);
        }

        [AllowAnonymous]
        public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(WarehouseLocationExcelDownloadDto input)
        {
            var downloadToken = await _excelDownloadTokenCache.GetAsync(input.DownloadToken);
            if (downloadToken == null || input.DownloadToken != downloadToken.Token)
            {
                throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
            }

            var warehouseLocations = await _warehouseLocationRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.Code, input.Description, input.Active, input.IdxMin, input.IdxMax, input.CountryId, input.WarehouseId);
            var items = warehouseLocations.Select(item => new
            {
                Code = item.WarehouseLocation.Code,
                Description = item.WarehouseLocation.Description,
                Active = item.WarehouseLocation.Active,
                Idx = item.WarehouseLocation.Idx,

                Country = item.Country?.Code,
                Warehouse = item.Warehouse?.Code,

            });

            var memoryStream = new MemoryStream();
            await memoryStream.SaveAsAsync(items);
            memoryStream.Seek(0, SeekOrigin.Begin);

            return new RemoteStreamContent(memoryStream, "WarehouseLocations.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        public virtual async Task<DemoTuan5.Shared.DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await _excelDownloadTokenCache.SetAsync(
                token,
                new WarehouseLocationExcelDownloadTokenCacheItem { Token = token },
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