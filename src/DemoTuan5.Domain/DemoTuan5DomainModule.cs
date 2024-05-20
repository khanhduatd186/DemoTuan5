using Volo.Abp.Caching;
using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace DemoTuan5;

[DependsOn(
    typeof(AbpDddDomainModule),
    typeof(AbpCachingModule),
    typeof(DemoTuan5DomainSharedModule)
)]
public class DemoTuan5DomainModule : AbpModule
{

}
