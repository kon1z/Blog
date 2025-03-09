using Meowv.Blog.Application.Dto;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Meowv.Blog.Admin.Pages.FriendLinks;

public partial class FriendLinkAdd
{
    private CreateFriendLinkInput input = new CreateFriendLinkInput();

    public async Task HandleSubmit()
    {
        if (string.IsNullOrWhiteSpace(input.Name) || string.IsNullOrWhiteSpace(input.Url)) return;

        var json = JsonSerializer.Serialize(input);

        var response = await GetResultAsync<BlogResponse>("api/meowv/blog/friendlink", json, HttpMethod.Post);
        if (response.Success)
        {
            await Message.Success("Successful", 0.5);
            NavigationManager.NavigateTo("/friendlinks/list");
        }
        else
        {
            await Message.Error(response.Message);
        }
    }
}