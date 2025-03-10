using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Meowv.Blog.Application.Dto;

namespace Meowv.Blog.Admin.Pages.Categories;

public partial class CategoryAdd
{
    private readonly CreateCategoryInput input = new();

    public async Task HandleSubmit()
    {
        if (string.IsNullOrWhiteSpace(input.Name) || string.IsNullOrWhiteSpace(input.Alias)) return;

        var json = JsonSerializer.Serialize(input);

        var response = await GetResultAsync<BlogResponse>("api/meowv/blog/category", json, HttpMethod.Post);
        if (response.Success)
        {
            await Message.Success("Successful", 0.5);
            NavigationManager.NavigateTo("/categories/list");
        }
        else
        {
            await Message.Error(response.Message);
        }
    }
}