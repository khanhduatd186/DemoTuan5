using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using DemoTuan5.Countries;
using Volo.Abp.Content;
using DemoTuan5.Shared;

namespace DemoTuan5.Countries
{
    [RemoteService(Name = "DemoTuan5")]
    [Area("demoTuan5")]
    [ControllerName("Country")]
    [Route("api/demo-tuan5/countries")]
    public abstract class CountryControllerBase : AbpController
    {
        protected ICountriesAppService _countriesAppService;

        public CountryControllerBase(ICountriesAppService countriesAppService)
        {
            _countriesAppService = countriesAppService;
        }

        [HttpGet]
        public virtual Task<PagedResultDto<CountryDto>> GetListAsync(GetCountriesInput input)
        {
            return _countriesAppService.GetListAsync(input);
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<CountryDto> GetAsync(Guid id)
        {
            return _countriesAppService.GetAsync(id);
        }

        [HttpPost]
        public virtual Task<CountryDto> CreateAsync(CountryCreateDto input)
        {
            return _countriesAppService.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<CountryDto> UpdateAsync(Guid id, CountryUpdateDto input)
        {
            return _countriesAppService.UpdateAsync(id, input);
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return _countriesAppService.DeleteAsync(id);
        }

        [HttpGet]
        [Route("as-excel-file")]
        public virtual Task<IRemoteStreamContent> GetListAsExcelFileAsync(CountryExcelDownloadDto input)
        {
            return _countriesAppService.GetListAsExcelFileAsync(input);
        }

        [HttpGet]
        [Route("download-token")]
        public virtual Task<DemoTuan5.Shared.DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            return _countriesAppService.GetDownloadTokenAsync();
        }
    }
}