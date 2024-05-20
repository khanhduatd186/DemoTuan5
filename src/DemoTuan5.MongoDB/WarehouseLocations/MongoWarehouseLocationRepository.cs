using DemoTuan5.Warehouses;
using DemoTuan5.Countries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using DemoTuan5.MongoDB;
using Volo.Abp.Domain.Repositories.MongoDB;
using Volo.Abp.MongoDB;
using MongoDB.Driver.Linq;
using MongoDB.Driver;

namespace DemoTuan5.WarehouseLocations
{
    public abstract class MongoWarehouseLocationRepositoryBase : MongoDbRepository<DemoTuan5MongoDbContext, WarehouseLocation, Guid>
    {
        public MongoWarehouseLocationRepositoryBase(IMongoDbContextProvider<DemoTuan5MongoDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        public virtual async Task<WarehouseLocationWithNavigationProperties> GetWithNavigationPropertiesAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var warehouseLocation = await (await GetMongoQueryableAsync(cancellationToken))
                .FirstOrDefaultAsync(e => e.Id == id, GetCancellationToken(cancellationToken));

            var country = await (await GetMongoQueryableAsync<Country>(cancellationToken)).FirstOrDefaultAsync(e => e.Id == warehouseLocation.CountryId, cancellationToken: cancellationToken);
            var warehouse = await (await GetMongoQueryableAsync<Warehouse>(cancellationToken)).FirstOrDefaultAsync(e => e.Id == warehouseLocation.WarehouseId, cancellationToken: cancellationToken);

            return new WarehouseLocationWithNavigationProperties
            {
                WarehouseLocation = warehouseLocation,
                Country = country,
                Warehouse = warehouse,

            };
        }

        public virtual async Task<List<WarehouseLocationWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
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
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetMongoQueryableAsync(cancellationToken)), filterText, code, description, active, idxMin, idxMax, countryId, warehouseId);
            var warehouseLocations = await query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? WarehouseLocationConsts.GetDefaultSorting(false) : sorting.Split('.').Last())
                .As<IMongoQueryable<WarehouseLocation>>()
                .PageBy<WarehouseLocation, IMongoQueryable<WarehouseLocation>>(skipCount, maxResultCount)
                .ToListAsync(GetCancellationToken(cancellationToken));

            var dbContext = await GetDbContextAsync(cancellationToken);
            return warehouseLocations.Select(s => new WarehouseLocationWithNavigationProperties
            {
                WarehouseLocation = s,
                Country = ApplyDataFilters<IMongoQueryable<Country>, Country>(dbContext.Collection<Country>().AsQueryable()).FirstOrDefault(e => e.Id == s.CountryId),
                Warehouse = ApplyDataFilters<IMongoQueryable<Warehouse>, Warehouse>(dbContext.Collection<Warehouse>().AsQueryable()).FirstOrDefault(e => e.Id == s.WarehouseId),

            }).ToList();
        }

        public virtual async Task<List<WarehouseLocation>> GetListAsync(
            string? filterText = null,
            string? code = null,
            string? description = null,
            bool? active = null,
            int? idxMin = null,
            int? idxMax = null,
            string? sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetMongoQueryableAsync(cancellationToken)), filterText, code, description, active, idxMin, idxMax);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? WarehouseLocationConsts.GetDefaultSorting(false) : sorting);
            return await query.As<IMongoQueryable<WarehouseLocation>>()
                .PageBy<WarehouseLocation, IMongoQueryable<WarehouseLocation>>(skipCount, maxResultCount)
                .ToListAsync(GetCancellationToken(cancellationToken));
        }

        public virtual async Task<long> GetCountAsync(
            string? filterText = null,
            string? code = null,
            string? description = null,
            bool? active = null,
            int? idxMin = null,
            int? idxMax = null,
            Guid? countryId = null,
            Guid? warehouseId = null,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetMongoQueryableAsync(cancellationToken)), filterText, code, description, active, idxMin, idxMax, countryId, warehouseId);
            return await query.As<IMongoQueryable<WarehouseLocation>>().LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<WarehouseLocation> ApplyFilter(
            IQueryable<WarehouseLocation> query,
            string? filterText = null,
            string? code = null,
            string? description = null,
            bool? active = null,
            int? idxMin = null,
            int? idxMax = null,
            Guid? countryId = null,
            Guid? warehouseId = null)
        {
            return query
                .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.Code!.Contains(filterText!) || e.Description!.Contains(filterText!))
                    .WhereIf(!string.IsNullOrWhiteSpace(code), e => e.Code.Contains(code))
                    .WhereIf(!string.IsNullOrWhiteSpace(description), e => e.Description.Contains(description))
                    .WhereIf(active.HasValue, e => e.Active == active)
                    .WhereIf(idxMin.HasValue, e => e.Idx >= idxMin!.Value)
                    .WhereIf(idxMax.HasValue, e => e.Idx <= idxMax!.Value)
                    .WhereIf(countryId != null && countryId != Guid.Empty, e => e.CountryId == countryId)
                    .WhereIf(warehouseId != null && warehouseId != Guid.Empty, e => e.WarehouseId == warehouseId);
        }
    }
}