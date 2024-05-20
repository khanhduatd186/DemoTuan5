using Volo.Abp.Modularity;

namespace DemoTuan5;

[DependsOn(
    typeof(DemoTuan5DomainModule),
    typeof(DemoTuan5TestBaseModule)
)]
public class DemoTuan5DomainTestModule : AbpModule
{

}
