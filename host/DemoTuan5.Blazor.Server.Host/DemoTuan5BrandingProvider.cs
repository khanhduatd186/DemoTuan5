using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace DemoTuan5.Blazor.Server.Host;

[Dependency(ReplaceServices = true)]
public class DemoTuan5BrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "DemoTuan5";
}
