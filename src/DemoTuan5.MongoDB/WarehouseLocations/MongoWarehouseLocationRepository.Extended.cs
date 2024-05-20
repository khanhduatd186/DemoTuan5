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
    public class MongoWarehouseLocationRepository : MongoWarehouseLocationRepositoryBase, IWarehouseLocationRepository
    {
        public MongoWarehouseLocationRepository(IMongoDbContextProvider<DemoTuan5MongoDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        //Write your custom code...
    }
}