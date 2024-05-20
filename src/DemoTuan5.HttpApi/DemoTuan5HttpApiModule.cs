using Localization.Resources.AbpUi;
using DemoTuan5.Localization;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace DemoTuan5;

[DependsOn(
    typeof(DemoTuan5ApplicationContractsModule),
    typeof(AbpAspNetCoreMvcModule))]
public class DemoTuan5HttpApiModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(DemoTuan5HttpApiModule).Assembly);
        });
    }
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Get<DemoTuan5Resource>()
                .AddBaseTypes(typeof(AbpUiResource));
        });
    }
}
