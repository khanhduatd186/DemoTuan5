using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.MultiTenancy;

namespace DemoTuan5.Seed;

public class DemoTuan5AuthServerDataSeedContributor : IDataSeedContributor, ITransientDependency
{
    private readonly DemoTuan5SampleIdentityDataSeeder _demoTuan5SampleIdentityDataSeeder;
    private readonly DemoTuan5AuthServerDataSeeder _demoTuan5AuthServerDataSeeder;
    private readonly ICurrentTenant _currentTenant;

    public DemoTuan5AuthServerDataSeedContributor(
        DemoTuan5AuthServerDataSeeder demoTuan5AuthServerDataSeeder,
        DemoTuan5SampleIdentityDataSeeder demoTuan5SampleIdentityDataSeeder,
        ICurrentTenant currentTenant)
    {
        _demoTuan5AuthServerDataSeeder = demoTuan5AuthServerDataSeeder;
        _demoTuan5SampleIdentityDataSeeder = demoTuan5SampleIdentityDataSeeder;
        _currentTenant = currentTenant;
    }

    public async Task SeedAsync(DataSeedContext context)
    {
        using (_currentTenant.Change(context?.TenantId))
        {
            await _demoTuan5SampleIdentityDataSeeder.SeedAsync(context!);
            await _demoTuan5AuthServerDataSeeder.SeedAsync(context!);
        }
    }
}
