using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using AntDesign;
using Meowv.Blog.Application.Dto;

namespace Meowv.Blog.Admin.Pages.Posts;

public partial class PostList
{
    private int limit = 10;
    private int page = 1;
    private IReadOnlyList<GetAdminPostDto> posts;
    private int total;

    protected override async Task OnInitializedAsync()
    {
        posts = await GetPostListAsync(page, limit);
    }

    public async Task HandlePageIndexChange(PaginationEventArgs args)
    {
        posts = await GetPostListAsync(page, limit);
    }

    public async Task<IReadOnlyList<GetAdminPostDto>> GetPostListAsync(int page, int limit)
    {
        var response =
            await GetResultAsync<BlogResponse<PagedList<GetAdminPostDto>>>(
                $"api/meowv/blog/admin/posts/{page}/{limit}");

        total = response.Result.Total;

        return response.Result.Item;
    }

    public void Goto(string id)
    {
        NavigationManager.NavigateTo($"/posts/edit/{id}");
    }

    public async Task DeleteAsync(string id)
    {
        var response = await GetResultAsync<BlogResponse>($"api/meowv/blog/post/{id}", method: HttpMethod.Delete);
        if (response.Success)
        {
            await Message.Success("Successful", 0.5);
            posts = await GetPostListAsync(page, limit);
        }
        else
        {
            await Message.Error(response.Message);
        }
    }
}