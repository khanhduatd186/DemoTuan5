using Volo.Abp.Autofac;
using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Modularity;

namespace DemoTuan5;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(DemoTuan5HttpApiClientModule),
    typeof(AbpHttpClientIdentityModelModule)
    )]
public class DemoTuan5ConsoleApiClientModule : AbpModule
{

}
