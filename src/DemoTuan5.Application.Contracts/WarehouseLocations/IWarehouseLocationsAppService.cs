using DemoTuan5.Shared;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;
using DemoTuan5.Shared;

namespace DemoTuan5.WarehouseLocations
{
    public partial interface IWarehouseLocationsAppService : IApplicationService
    {

        Task<PagedResultDto<WarehouseLocationWithNavigationPropertiesDto>> GetListAsync(GetWarehouseLocationsInput input);

        Task<WarehouseLocationWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id);

        Task<WarehouseLocationDto> GetAsync(Guid id);

        Task<PagedResultDto<LookupDto<Guid>>> GetCountryLookupAsync(LookupRequestDto input);

        Task<PagedResultDto<LookupDto<Guid>>> GetWarehouseLookupAsync(LookupRequestDto input);

        Task DeleteAsync(Guid id);

        Task<WarehouseLocationDto> CreateAsync(WarehouseLocationCreateDto input);

        Task<WarehouseLocationDto> UpdateAsync(Guid id, WarehouseLocationUpdateDto input);

        Task<IRemoteStreamContent> GetListAsExcelFileAsync(WarehouseLocationExcelDownloadDto input);

        Task<DemoTuan5.Shared.DownloadTokenResultDto> GetDownloadTokenAsync();
    }
}