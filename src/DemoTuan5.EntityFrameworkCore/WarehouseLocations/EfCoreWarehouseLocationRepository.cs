using DemoTuan5.Warehouses;
using DemoTuan5.Countries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using DemoTuan5.EntityFrameworkCore;

namespace DemoTuan5.WarehouseLocations
{
    public abstract class EfCoreWarehouseLocationRepositoryBase : EfCoreRepository<DemoTuan5DbContext, WarehouseLocation, Guid>
    {
        public EfCoreWarehouseLocationRepositoryBase(IDbContextProvider<DemoTuan5DbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public virtual async Task<WarehouseLocationWithNavigationProperties> GetWithNavigationPropertiesAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var dbContext = await GetDbContextAsync();

            return (await GetDbSetAsync()).Where(b => b.Id == id)
                .Select(warehouseLocation => new WarehouseLocationWithNavigationProperties
                {
                    WarehouseLocation = warehouseLocation,
                    Country = dbContext.Set<Country>().FirstOrDefault(c => c.Id == warehouseLocation.CountryId),
                    Warehouse = dbContext.Set<Warehouse>().FirstOrDefault(c => c.Id == warehouseLocation.WarehouseId)
                }).FirstOrDefault();
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
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, code, description, active, idxMin, idxMax, countryId, warehouseId);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? WarehouseLocationConsts.GetDefaultSorting(true) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        protected virtual async Task<IQueryable<WarehouseLocationWithNavigationProperties>> GetQueryForNavigationPropertiesAsync()
        {
            return from warehouseLocation in (await GetDbSetAsync())
                   join country in (await GetDbContextAsync()).Set<Country>() on warehouseLocation.CountryId equals country.Id into countries
                   from country in countries.DefaultIfEmpty()
                   join warehouse in (await GetDbContextAsync()).Set<Warehouse>() on warehouseLocation.WarehouseId equals warehouse.Id into warehouses
                   from warehouse in warehouses.DefaultIfEmpty()
                   select new WarehouseLocationWithNavigationProperties
                   {
                       WarehouseLocation = warehouseLocation,
                       Country = country,
                       Warehouse = warehouse
                   };
        }

        protected virtual IQueryable<WarehouseLocationWithNavigationProperties> ApplyFilter(
            IQueryable<WarehouseLocationWithNavigationProperties> query,
            string? filterText,
            string? code = null,
            string? description = null,
            bool? active = null,
            int? idxMin = null,
            int? idxMax = null,
            Guid? countryId = null,
            Guid? warehouseId = null)
        {
            return query
                .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.WarehouseLocation.Code!.Contains(filterText!) || e.WarehouseLocation.Description!.Contains(filterText!))
                    .WhereIf(!string.IsNullOrWhiteSpace(code), e => e.WarehouseLocation.Code.Contains(code))
                    .WhereIf(!string.IsNullOrWhiteSpace(description), e => e.WarehouseLocation.Description.Contains(description))
                    .WhereIf(active.HasValue, e => e.WarehouseLocation.Active == active)
                    .WhereIf(idxMin.HasValue, e => e.WarehouseLocation.Idx >= idxMin!.Value)
                    .WhereIf(idxMax.HasValue, e => e.WarehouseLocation.Idx <= idxMax!.Value)
                    .WhereIf(countryId != null && countryId != Guid.Empty, e => e.Country != null && e.Country.Id == countryId)
                    .WhereIf(warehouseId != null && warehouseId != Guid.Empty, e => e.Warehouse != null && e.Warehouse.Id == warehouseId);
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
            var query = ApplyFilter((await GetQueryableAsync()), filterText, code, description, active, idxMin, idxMax);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? WarehouseLocationConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
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
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, code, description, active, idxMin, idxMax, countryId, warehouseId);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<WarehouseLocation> ApplyFilter(
            IQueryable<WarehouseLocation> query,
            string? filterText = null,
            string? code = null,
            string? description = null,
            bool? active = null,
            int? idxMin = null,
            int? idxMax = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.Code!.Contains(filterText!) || e.Description!.Contains(filterText!))
                    .WhereIf(!string.IsNullOrWhiteSpace(code), e => e.Code.Contains(code))
                    .WhereIf(!string.IsNullOrWhiteSpace(description), e => e.Description.Contains(description))
                    .WhereIf(active.HasValue, e => e.Active == active)
                    .WhereIf(idxMin.HasValue, e => e.Idx >= idxMin!.Value)
                    .WhereIf(idxMax.HasValue, e => e.Idx <= idxMax!.Value);
        }
    }
}