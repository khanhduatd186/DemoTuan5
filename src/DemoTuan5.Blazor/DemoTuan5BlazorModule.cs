using Microsoft.Extensions.DependencyInjection;
using DemoTuan5.Blazor.Menus;
using Volo.Abp.AspNetCore.Components.Web.Theming;
using Volo.Abp.AspNetCore.Components.Web.Theming.Routing;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.UI.Navigation;

namespace DemoTuan5.Blazor;

[DependsOn(
    typeof(DemoTuan5ApplicationContractsModule),
    typeof(AbpAspNetCoreComponentsWebThemingModule),
    typeof(AbpAutoMapperModule)
    )]
public class DemoTuan5BlazorModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAutoMapperObjectMapper<DemoTuan5BlazorModule>();

        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddProfile<DemoTuan5BlazorAutoMapperProfile>(validate: true);
        });

        Configure<AbpNavigationOptions>(options =>
        {
            options.MenuContributors.Add(new DemoTuan5MenuContributor());
        });

        Configure<AbpRouterOptions>(options =>
        {
            options.AdditionalAssemblies.Add(typeof(DemoTuan5BlazorModule).Assembly);
        });

        context.Services.AddDevExpressBlazor();
    }
}
