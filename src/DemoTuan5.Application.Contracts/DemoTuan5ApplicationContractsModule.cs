using Volo.Abp.Application;
using Volo.Abp.Authorization;
using Volo.Abp.Modularity;

namespace DemoTuan5;

[DependsOn(
    typeof(DemoTuan5DomainSharedModule),
    typeof(AbpDddApplicationContractsModule),
    typeof(AbpAuthorizationAbstractionsModule)
    )]
public class DemoTuan5ApplicationContractsModule : AbpModule
{

}
