using DemoTuan5.WarehouseLocations;
using DemoTuan5.Warehouses;
using DemoTuan5.Countries;
using MongoDB.Driver;
using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace DemoTuan5.MongoDB;

[ConnectionStringName(DemoTuan5DbProperties.ConnectionStringName)]
public class DemoTuan5MongoDbContext : AbpMongoDbContext, IDemoTuan5MongoDbContext
{
    public IMongoCollection<WarehouseLocation> WarehouseLocations => Collection<WarehouseLocation>();
    public IMongoCollection<Warehouse> Warehouses => Collection<Warehouse>();
    public IMongoCollection<Country> Countries => Collection<Country>();
    /* Add mongo collections here. Example:
     * public IMongoCollection<Question> Questions => Collection<Question>();
     */

    protected override void CreateModel(IMongoModelBuilder modelBuilder)
    {
        base.CreateModel(modelBuilder);

        modelBuilder.ConfigureDemoTuan5();

        modelBuilder.Entity<Country>(b => { b.CollectionName = DemoTuan5DbProperties.DbTablePrefix + "Countries"; });

        modelBuilder.Entity<Warehouse>(b => { b.CollectionName = DemoTuan5DbProperties.DbTablePrefix + "Warehouses"; });

        modelBuilder.Entity<WarehouseLocation>(b => { b.CollectionName = DemoTuan5DbProperties.DbTablePrefix + "WarehouseLocations"; });
    }
}