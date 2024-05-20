using Microsoft.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;

namespace DemoTuan5.EntityFrameworkCore;

public class DemoTuan5HttpApiHostMigrationsDbContext : AbpDbContext<DemoTuan5HttpApiHostMigrationsDbContext>
{
    public DemoTuan5HttpApiHostMigrationsDbContext(DbContextOptions<DemoTuan5HttpApiHostMigrationsDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ConfigureDemoTuan5();
        modelBuilder.ConfigureAuditLogging();
        modelBuilder.ConfigurePermissionManagement();
        modelBuilder.ConfigureSettingManagement();
    }
}
