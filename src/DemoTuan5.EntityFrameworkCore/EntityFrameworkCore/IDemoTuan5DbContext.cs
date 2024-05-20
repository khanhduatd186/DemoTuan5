using DemoTuan5.WarehouseLocations;
using DemoTuan5.Warehouses;
using DemoTuan5.Countries;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace DemoTuan5.EntityFrameworkCore;

[ConnectionStringName(DemoTuan5DbProperties.ConnectionStringName)]
public interface IDemoTuan5DbContext : IEfCoreDbContext
{
    DbSet<WarehouseLocation> WarehouseLocations { get; set; }
    DbSet<Warehouse> Warehouses { get; set; }
    DbSet<Country> Countries { get; set; }
    /* Add DbSet for each Aggregate Root here. Example:
     * DbSet<Question> Questions { get; }
     */
}