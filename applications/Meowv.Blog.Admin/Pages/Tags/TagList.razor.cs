using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Meowv.Blog.Application.Dto;

namespace Meowv.Blog.Admin.Pages.Tags;

public partial class TagList
{
    private readonly UpdateTagInput input = new();

    private string tagId;
    private List<GetAdminTagDto> tags;

    private bool visible;

    protected override async Task OnInitializedAsync()
    {
        tags = await GetTagListAsync();
    }

    public async Task<List<GetAdminTagDto>> GetTagListAsync()
    {
        var response = await GetResultAsync<BlogResponse<List<GetAdminTagDto>>>("api/meowv/blog/admin/tags");
        return response.Result;
    }

    public async Task DeleteAsync(string id)
    {
        var response = await GetResultAsync<BlogResponse>($"api/meowv/blog/tag/{id}", method: HttpMethod.Delete);
        if (response.Success)
        {
            await Message.Success("Successful", 0.5);
            tags = await GetTagListAsync();
        }
        else
        {
            await Message.Error(response.Message);
        }
    }

    private void Close()
    {
        visible = false;
    }

    private void Open(GetAdminTagDto dto)
    {
        tagId = dto.Id;
        input.Name = dto.Name;
        input.Alias = dto.Alias;

        visible = true;
    }

    public async Task HandleSubmit()
    {
        if (string.IsNullOrWhiteSpace(input.Name) || string.IsNullOrWhiteSpace(input.Alias)) return;

        var json = JsonSerializer.Serialize(input);

        var response = await GetResultAsync<BlogResponse>($"api/meowv/blog/tag/{tagId}", json, HttpMethod.Put);
        if (response.Success)
        {
            Close();
            await Message.Success("Successful", 0.5);
            tags = await GetTagListAsync();
        }
        else
        {
            await Message.Error(response.Message);
        }
    }
}