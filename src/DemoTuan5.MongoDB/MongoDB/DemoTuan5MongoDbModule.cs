using DemoTuan5.WarehouseLocations;
using DemoTuan5.Warehouses;
using DemoTuan5.Countries;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;
using Volo.Abp.MongoDB;

namespace DemoTuan5.MongoDB;

[DependsOn(
    typeof(DemoTuan5DomainModule),
    typeof(AbpMongoDbModule)
    )]
public class DemoTuan5MongoDbModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddMongoDbContext<DemoTuan5MongoDbContext>(options =>
        {
            /* Add custom repositories here. Example:
             * options.AddRepository<Question, MongoQuestionRepository>();
             */
            options.AddRepository<Country, Countries.MongoCountryRepository>();

            options.AddRepository<Warehouse, Warehouses.MongoWarehouseRepository>();

            options.AddRepository<WarehouseLocation, WarehouseLocations.MongoWarehouseLocationRepository>();

        });
    }
}