using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.MultiTenancy;

namespace DemoTuan5.Seed;

public class DemoTuan5HttpApiHostDataSeedContributor : IDataSeedContributor, ITransientDependency
{
    private readonly DemoTuan5SampleDataSeeder _demoTuan5SampleDataSeeder;
    private readonly ICurrentTenant _currentTenant;

    public DemoTuan5HttpApiHostDataSeedContributor(
        DemoTuan5SampleDataSeeder demoTuan5SampleDataSeeder,
        ICurrentTenant currentTenant)
    {
        _demoTuan5SampleDataSeeder = demoTuan5SampleDataSeeder;
        _currentTenant = currentTenant;
    }

    public async Task SeedAsync(DataSeedContext context)
    {
        using (_currentTenant.Change(context?.TenantId))
        {
            await _demoTuan5SampleDataSeeder.SeedAsync(context!);
        }
    }
}
