using System.Net.Http;
using System.Threading.Tasks;
using Meowv.Blog.Application.Dto;

namespace Meowv.Blog.Web.Pages.Posts;

public class PostModel : PageBase
{
    public PostModel(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
    {
    }

    public BlogResponse<PostDetailDto> Post { get; set; }

    public async Task OnGetAsync(string url)
    {
        Post = await GetResultAsync<BlogResponse<PostDetailDto>>($"api/meowv/blog/post?url={url}");
    }
}