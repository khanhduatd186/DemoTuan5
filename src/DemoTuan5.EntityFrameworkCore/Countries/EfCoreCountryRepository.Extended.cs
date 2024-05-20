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

namespace DemoTuan5.Countries
{
    public class EfCoreCountryRepository : EfCoreCountryRepositoryBase, ICountryRepository
    {
        public EfCoreCountryRepository(IDbContextProvider<DemoTuan5DbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }
    }
}