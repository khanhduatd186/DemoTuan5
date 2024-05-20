using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;

namespace DemoTuan5.Pages;

public class IndexModel : DemoTuan5PageModel
{
    public void OnGet()
    {

    }

    public async Task OnPostLoginAsync()
    {
        await HttpContext.ChallengeAsync("oidc");
    }
}
