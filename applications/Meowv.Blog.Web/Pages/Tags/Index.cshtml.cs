using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Meowv.Blog.Application.Dto;

namespace Meowv.Blog.Web.Pages.Tags;

public class IndexModel : PageBase
{
    public IndexModel(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
    {
    }

    public BlogResponse<List<GetTagDto>> Tags { get; set; }

    public async Task OnGetAsync()
    {
        Tags = await GetResultAsync<BlogResponse<List<GetTagDto>>>("api/meowv/blog/tags");
    }
}