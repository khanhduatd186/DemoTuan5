using DemoTuan5.WarehouseLocations;
using DemoTuan5.Warehouses;
using DemoTuan5.Countries;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace DemoTuan5.EntityFrameworkCore;

[ConnectionStringName(DemoTuan5DbProperties.ConnectionStringName)]
public class DemoTuan5DbContext : AbpDbContext<DemoTuan5DbContext>, IDemoTuan5DbContext
{
    public DbSet<WarehouseLocation> WarehouseLocations { get; set; } = null!;
    public DbSet<Warehouse> Warehouses { get; set; } = null!;
    public DbSet<Country> Countries { get; set; } = null!;
    /* Add DbSet for each Aggregate Root here. Example:
     * public DbSet<Question> Questions { get; set; }
     */

    public DemoTuan5DbContext(DbContextOptions<DemoTuan5DbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ConfigureDemoTuan5();
    }
}