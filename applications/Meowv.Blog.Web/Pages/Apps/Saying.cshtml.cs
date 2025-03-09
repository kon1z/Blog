using System.Net.Http;
using System.Threading.Tasks;
using Meowv.Blog.Application.Dto;

namespace Meowv.Blog.Web.Pages.Apps;

public class SayingModel : PageBase
{
    public SayingModel(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
    {
    }

    public string Saying { get; set; }

    public async Task OnGetAsync()
    {
        var response = await GetResultAsync<BlogResponse<string>>("api/meowv/saying/random");
        Saying = response.Result;
    }
}