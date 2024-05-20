using DemoTuan5.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace DemoTuan5;

public abstract class DemoTuan5Controller : AbpControllerBase
{
    protected DemoTuan5Controller()
    {
        LocalizationResource = typeof(DemoTuan5Resource);
    }
}
