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
    public class EfCoreWarehouseLocationRepository : EfCoreWarehouseLocationRepositoryBase, IWarehouseLocationRepository
    {
        public EfCoreWarehouseLocationRepository(IDbContextProvider<DemoTuan5DbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }
    }
}