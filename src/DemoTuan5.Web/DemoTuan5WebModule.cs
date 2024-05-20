using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.DependencyInjection;
using DemoTuan5.Localization;
using DemoTuan5.Web.Menus;
using Volo.Abp.AspNetCore.Mvc.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.UI.Navigation;
using Volo.Abp.VirtualFileSystem;
using DemoTuan5.Permissions;

namespace DemoTuan5.Web;

[DependsOn(
    typeof(DemoTuan5ApplicationContractsModule),
    typeof(AbpAspNetCoreMvcUiThemeSharedModule),
    typeof(AbpAutoMapperModule)
    )]
public class DemoTuan5WebModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.PreConfigure<AbpMvcDataAnnotationsLocalizationOptions>(options =>
        {
            options.AddAssemblyResource(typeof(DemoTuan5Resource), typeof(DemoTuan5WebModule).Assembly);
        });

        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(DemoTuan5WebModule).Assembly);
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpNavigationOptions>(options =>
        {
            options.MenuContributors.Add(new DemoTuan5MenuContributor());
        });

        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<DemoTuan5WebModule>();
        });

        context.Services.AddAutoMapperObjectMapper<DemoTuan5WebModule>();
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<DemoTuan5WebModule>(validate: true);
        });

        Configure<RazorPagesOptions>(options =>
        {
            //Configure authorization.
            options.Conventions.AuthorizePage("/Countries/Index", DemoTuan5Permissions.Countries.Default);
            options.Conventions.AuthorizePage("/Warehouses/Index", DemoTuan5Permissions.Warehouses.Default);
            options.Conventions.AuthorizePage("/WarehouseLocations/Index", DemoTuan5Permissions.WarehouseLocations.Default);
        });
    }
}