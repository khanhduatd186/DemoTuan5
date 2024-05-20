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

namespace DemoTuan5.Warehouses
{
    public abstract class MongoWarehouseRepositoryBase : MongoDbRepository<DemoTuan5MongoDbContext, Warehouse, Guid>
    {
        public MongoWarehouseRepositoryBase(IMongoDbContextProvider<DemoTuan5MongoDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        public virtual async Task<List<Warehouse>> GetListAsync(
            string? filterText = null,
            string? code = null,
            string? description = null,
            bool? active = null,
            string? sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetMongoQueryableAsync(cancellationToken)), filterText, code, description, active);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? WarehouseConsts.GetDefaultSorting(false) : sorting);
            return await query.As<IMongoQueryable<Warehouse>>()
                .PageBy<Warehouse, IMongoQueryable<Warehouse>>(skipCount, maxResultCount)
                .ToListAsync(GetCancellationToken(cancellationToken));
        }

        public virtual async Task<long> GetCountAsync(
            string? filterText = null,
            string? code = null,
            string? description = null,
            bool? active = null,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetMongoQueryableAsync(cancellationToken)), filterText, code, description, active);
            return await query.As<IMongoQueryable<Warehouse>>().LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<Warehouse> ApplyFilter(
            IQueryable<Warehouse> query,
            string? filterText = null,
            string? code = null,
            string? description = null,
            bool? active = null)
        {
            return query
                .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.Code!.Contains(filterText!) || e.Description!.Contains(filterText!))
                    .WhereIf(!string.IsNullOrWhiteSpace(code), e => e.Code.Contains(code))
                    .WhereIf(!string.IsNullOrWhiteSpace(description), e => e.Description.Contains(description))
                    .WhereIf(active.HasValue, e => e.Active == active);
        }
    }
}