using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;

namespace DemoTuan5;

[Dependency(ReplaceServices = true)]
public class DemoTuan5BrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "DemoTuan5";
}
