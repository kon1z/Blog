using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Meowv.Blog.Application.Dto;

namespace Meowv.Blog.Web.Pages.Posts;

public class TagModel : PageBase
{
    public TagModel(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
    {
    }

    public BlogResponse<List<GetPostDto>> Posts { get; set; }

    public async Task OnGetAsync(string tag)
    {
        Posts = await GetResultAsync<BlogResponse<List<GetPostDto>>>($"api/meowv/blog/posts/tag/{tag}");
    }
}