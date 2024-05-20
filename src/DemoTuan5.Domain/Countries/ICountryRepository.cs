using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace DemoTuan5.Countries
{
    public partial interface ICountryRepository : IRepository<Country, Guid>
    {
        Task<List<Country>> GetListAsync(
            string? filterText = null,
            string? code = null,
            string? description = null,
            string? sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default
        );

        Task<long> GetCountAsync(
            string? filterText = null,
            string? code = null,
            string? description = null,
            CancellationToken cancellationToken = default);
    }
}