using Volo.Abp.AspNetCore.Components.Server.Theming;
using Volo.Abp.Modularity;

namespace DemoTuan5.Blazor.Server;

[DependsOn(
    typeof(DemoTuan5BlazorModule),
    typeof(AbpAspNetCoreComponentsServerThemingModule)
    )]
public class DemoTuan5BlazorServerModule : AbpModule
{

}
