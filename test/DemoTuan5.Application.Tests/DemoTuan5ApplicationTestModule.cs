using Volo.Abp.Modularity;

namespace DemoTuan5;

[DependsOn(
    typeof(DemoTuan5ApplicationModule),
    typeof(DemoTuan5DomainTestModule)
    )]
public class DemoTuan5ApplicationTestModule : AbpModule
{

}
