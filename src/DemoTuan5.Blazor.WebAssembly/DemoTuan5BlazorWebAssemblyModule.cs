using Volo.Abp.AspNetCore.Components.WebAssembly.Theming;
using Volo.Abp.Modularity;

namespace DemoTuan5.Blazor.WebAssembly;

[DependsOn(
    typeof(DemoTuan5BlazorModule),
    typeof(DemoTuan5HttpApiClientModule),
    typeof(AbpAspNetCoreComponentsWebAssemblyThemingModule)
)]
public class DemoTuan5BlazorWebAssemblyModule : AbpModule
{

}
