using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace DemoTuan5;

[DependsOn(
    typeof(DemoTuan5ApplicationContractsModule),
    typeof(AbpHttpClientModule))]
public class DemoTuan5HttpApiClientModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddHttpClientProxies(
            typeof(DemoTuan5ApplicationContractsModule).Assembly,
            DemoTuan5RemoteServiceConsts.RemoteServiceName
        );

        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<DemoTuan5HttpApiClientModule>();
        });
    }
}
