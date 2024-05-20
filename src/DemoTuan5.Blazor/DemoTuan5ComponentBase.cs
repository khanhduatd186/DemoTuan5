using DemoTuan5.Localization;
using Volo.Abp.AspNetCore.Components;

namespace DemoTuan5.Blazor;

public abstract class DemoTuan5ComponentBase : AbpComponentBase
{
    protected DemoTuan5ComponentBase()
    {
        LocalizationResource = typeof(DemoTuan5Resource);
    }
}
