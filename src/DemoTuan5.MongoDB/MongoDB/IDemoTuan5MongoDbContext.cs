using DemoTuan5.WarehouseLocations;
using DemoTuan5.Warehouses;
using DemoTuan5.Countries;
using MongoDB.Driver;
using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace DemoTuan5.MongoDB;

[ConnectionStringName(DemoTuan5DbProperties.ConnectionStringName)]
public interface IDemoTuan5MongoDbContext : IAbpMongoDbContext
{
    IMongoCollection<WarehouseLocation> WarehouseLocations { get; }
    IMongoCollection<Warehouse> Warehouses { get; }
    IMongoCollection<Country> Countries { get; }
    /* Define mongo collections here. Example:
     * IMongoCollection<Question> Questions { get; }
     */
}