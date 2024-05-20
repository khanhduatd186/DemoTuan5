using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace DemoTuan5.WarehouseLocations
{
    public partial interface IWarehouseLocationRepository : IRepository<WarehouseLocation, Guid>
    {
        Task<WarehouseLocationWithNavigationProperties> GetWithNavigationPropertiesAsync(
    Guid id,
    CancellationToken cancellationToken = default
);

        Task<List<WarehouseLocationWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string? filterText = null,
            string? code = null,
            string? description = null,
            bool? active = null,
            int? idxMin = null,
            int? idxMax = null,
            Guid? countryId = null,
            Guid? warehouseId = null,
            string? sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default
        );

        Task<List<WarehouseLocation>> GetListAsync(
                    string? filterText = null,
                    string? code = null,
                    string? description = null,
                    bool? active = null,
                    int? idxMin = null,
                    int? idxMax = null,
                    string? sorting = null,
                    int maxResultCount = int.MaxValue,
                    int skipCount = 0,
                    CancellationToken cancellationToken = default
                );

        Task<long> GetCountAsync(
            string? filterText = null,
            string? code = null,
            string? description = null,
            bool? active = null,
            int? idxMin = null,
            int? idxMax = null,
            Guid? countryId = null,
            Guid? warehouseId = null,
            CancellationToken cancellationToken = default);
    }
}