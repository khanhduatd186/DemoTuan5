using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;
using DemoTuan5.Shared;

namespace DemoTuan5.Warehouses
{
    public partial interface IWarehousesAppService : IApplicationService
    {

        Task<PagedResultDto<WarehouseDto>> GetListAsync(GetWarehousesInput input);

        Task<WarehouseDto> GetAsync(Guid id);

        Task DeleteAsync(Guid id);

        Task<WarehouseDto> CreateAsync(WarehouseCreateDto input);

        Task<WarehouseDto> UpdateAsync(Guid id, WarehouseUpdateDto input);

        Task<IRemoteStreamContent> GetListAsExcelFileAsync(WarehouseExcelDownloadDto input);

        Task<DemoTuan5.Shared.DownloadTokenResultDto> GetDownloadTokenAsync();
    }
}