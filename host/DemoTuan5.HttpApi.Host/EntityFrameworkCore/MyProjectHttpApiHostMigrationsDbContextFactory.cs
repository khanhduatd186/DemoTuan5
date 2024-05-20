using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace DemoTuan5.EntityFrameworkCore;

public class DemoTuan5HttpApiHostMigrationsDbContextFactory : IDesignTimeDbContextFactory<DemoTuan5HttpApiHostMigrationsDbContext>
{
    public DemoTuan5HttpApiHostMigrationsDbContext CreateDbContext(string[] args)
    {
        var configuration = BuildConfiguration();

        var builder = new DbContextOptionsBuilder<DemoTuan5HttpApiHostMigrationsDbContext>()
            .UseNpgsql(configuration.GetConnectionString("DemoTuan5"));

        return new DemoTuan5HttpApiHostMigrationsDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}
