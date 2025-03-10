﻿using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using AntDesign;
using Meowv.Blog.Application.Dto;

namespace Meowv.Blog.Admin.Pages.Sayings;

public partial class SayingList
{
    private int limit = 10;
    private int page = 1;
    private IReadOnlyList<SayingDto> sayings;
    private int total;
    private string values;

    private bool visible;

    protected override async Task OnInitializedAsync()
    {
        sayings = await GetSayingListAsync(page, limit);
    }

    public async Task HandlePageIndexChange(PaginationEventArgs args)
    {
        sayings = await GetSayingListAsync(page, limit);
    }

    public async Task<IReadOnlyList<SayingDto>> GetSayingListAsync(int page, int limit)
    {
        var response = await GetResultAsync<BlogResponse<PagedList<SayingDto>>>($"api/meowv/sayings/{page}/{limit}");

        total = response.Result.Total;

        return response.Result.Item;
    }

    public async Task DeleteAsync(string id)
    {
        var response = await GetResultAsync<BlogResponse>($"api/meowv/saying/{id}", method: HttpMethod.Delete);
        if (response.Success)
        {
            await Message.Success("Successful", 0.5);
            sayings = await GetSayingListAsync(page, limit);
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

    private void Open()
    {
        visible = true;
    }

    public async Task OnSubmit()
    {
        if (string.IsNullOrWhiteSpace(values))
        {
            await Message.Info("鸡汤鸡汤鸡汤");
            return;
        }

        var json = JsonSerializer.Serialize(new { content = values.Split("\n") });
        var response = await GetResultAsync<BlogResponse>("api/meowv/saying", json, HttpMethod.Post);
        if (response.Success)
        {
            Close();
            await Message.Success("Successful", 0.5);
            sayings = await GetSayingListAsync(page, limit);
        }
        else
        {
            await Message.Error(response.Message);
        }
    }
}