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
using DemoTuan5.Countries;
using MiniExcelLibs;
using Volo.Abp.Content;
using Volo.Abp.Authorization;
using Volo.Abp.Caching;
using Microsoft.Extensions.Caching.Distributed;
using DemoTuan5.Shared;

namespace DemoTuan5.Countries
{

    [Authorize(DemoTuan5Permissions.Countries.Default)]
    public abstract class CountriesAppServiceBase : ApplicationService
    {
        protected IDistributedCache<CountryExcelDownloadTokenCacheItem, string> _excelDownloadTokenCache;
        protected ICountryRepository _countryRepository;
        protected CountryManager _countryManager;

        public CountriesAppServiceBase(ICountryRepository countryRepository, CountryManager countryManager, IDistributedCache<CountryExcelDownloadTokenCacheItem, string> excelDownloadTokenCache)
        {
            _excelDownloadTokenCache = excelDownloadTokenCache;
            _countryRepository = countryRepository;
            _countryManager = countryManager;
        }

        public virtual async Task<PagedResultDto<CountryDto>> GetListAsync(GetCountriesInput input)
        {
            var totalCount = await _countryRepository.GetCountAsync(input.FilterText, input.Code, input.Description);
            var items = await _countryRepository.GetListAsync(input.FilterText, input.Code, input.Description, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<CountryDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<Country>, List<CountryDto>>(items)
            };
        }

        public virtual async Task<CountryDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<Country, CountryDto>(await _countryRepository.GetAsync(id));
        }

        [Authorize(DemoTuan5Permissions.Countries.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _countryRepository.DeleteAsync(id);
        }

        [Authorize(DemoTuan5Permissions.Countries.Create)]
        public virtual async Task<CountryDto> CreateAsync(CountryCreateDto input)
        {

            var country = await _countryManager.CreateAsync(
            input.Code, input.Description
            );

            return ObjectMapper.Map<Country, CountryDto>(country);
        }

        [Authorize(DemoTuan5Permissions.Countries.Edit)]
        public virtual async Task<CountryDto> UpdateAsync(Guid id, CountryUpdateDto input)
        {

            var country = await _countryManager.UpdateAsync(
            id,
            input.Code, input.Description, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<Country, CountryDto>(country);
        }

        [AllowAnonymous]
        public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(CountryExcelDownloadDto input)
        {
            var downloadToken = await _excelDownloadTokenCache.GetAsync(input.DownloadToken);
            if (downloadToken == null || input.DownloadToken != downloadToken.Token)
            {
                throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
            }

            var items = await _countryRepository.GetListAsync(input.FilterText, input.Code, input.Description);

            var memoryStream = new MemoryStream();
            await memoryStream.SaveAsAsync(ObjectMapper.Map<List<Country>, List<CountryExcelDto>>(items));
            memoryStream.Seek(0, SeekOrigin.Begin);

            return new RemoteStreamContent(memoryStream, "Countries.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        public virtual async Task<DemoTuan5.Shared.DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await _excelDownloadTokenCache.SetAsync(
                token,
                new CountryExcelDownloadTokenCacheItem { Token = token },
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