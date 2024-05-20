using DemoTuan5.Localization;
using Volo.Abp.Application.Services;

namespace DemoTuan5;

public abstract class DemoTuan5AppService : ApplicationService
{
    protected DemoTuan5AppService()
    {
        LocalizationResource = typeof(DemoTuan5Resource);
        ObjectMapperContext = typeof(DemoTuan5ApplicationModule);
    }
}
