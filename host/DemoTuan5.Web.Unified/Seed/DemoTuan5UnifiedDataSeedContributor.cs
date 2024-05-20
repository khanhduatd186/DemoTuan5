using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Uow;

namespace DemoTuan5.Seed;

public class DemoTuan5UnifiedDataSeedContributor : IDataSeedContributor, ITransientDependency
{
    private readonly DemoTuan5SampleIdentityDataSeeder _sampleIdentityDataSeeder;
    private readonly DemoTuan5SampleDataSeeder _demoTuan5SampleDataSeeder;
    private readonly IUnitOfWorkManager _unitOfWorkManager;
    private readonly ICurrentTenant _currentTenant;

    public DemoTuan5UnifiedDataSeedContributor(
        DemoTuan5SampleIdentityDataSeeder sampleIdentityDataSeeder,
        IUnitOfWorkManager unitOfWorkManager,
        DemoTuan5SampleDataSeeder demoTuan5SampleDataSeeder,
        ICurrentTenant currentTenant)
    {
        _sampleIdentityDataSeeder = sampleIdentityDataSeeder;
        _unitOfWorkManager = unitOfWorkManager;
        _demoTuan5SampleDataSeeder = demoTuan5SampleDataSeeder;
        _currentTenant = currentTenant;
    }

    public async Task SeedAsync(DataSeedContext context)
    {
        await _unitOfWorkManager.Current!.SaveChangesAsync();

        using (_currentTenant.Change(context.TenantId))
        {
            await _sampleIdentityDataSeeder.SeedAsync(context);
            await _unitOfWorkManager.Current.SaveChangesAsync();
            await _demoTuan5SampleDataSeeder.SeedAsync(context);
        }
    }
}
