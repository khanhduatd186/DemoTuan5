using DemoTuan5.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace DemoTuan5.Pages;

public abstract class DemoTuan5PageModel : AbpPageModel
{
    protected DemoTuan5PageModel()
    {
        LocalizationResourceType = typeof(DemoTuan5Resource);
    }
}
