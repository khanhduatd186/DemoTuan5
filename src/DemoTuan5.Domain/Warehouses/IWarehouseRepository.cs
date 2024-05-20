using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace DemoTuan5.Warehouses
{
    public partial interface IWarehouseRepository : IRepository<Warehouse, Guid>
    {
        Task<List<Warehouse>> GetListAsync(
            string? filterText = null,
            string? code = null,
            string? description = null,
            bool? active = null,
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
            CancellationToken cancellationToken = default);
    }
}