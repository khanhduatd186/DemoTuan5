using DemoTuan5.WarehouseLocations;
using DemoTuan5.Warehouses;
using DemoTuan5.Countries;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace DemoTuan5.EntityFrameworkCore;

[DependsOn(
    typeof(DemoTuan5DomainModule),
    typeof(AbpEntityFrameworkCoreModule)
)]
public class DemoTuan5EntityFrameworkCoreModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAbpDbContext<DemoTuan5DbContext>(options =>
        {
            /* Add custom repositories here. Example:
             * options.AddRepository<Question, EfCoreQuestionRepository>();
             */
            options.AddRepository<Country, Countries.EfCoreCountryRepository>();

            options.AddRepository<Warehouse, Warehouses.EfCoreWarehouseRepository>();

            options.AddRepository<WarehouseLocation, WarehouseLocations.EfCoreWarehouseLocationRepository>();

        });
    }
}