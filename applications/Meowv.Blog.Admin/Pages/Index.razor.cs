using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Meowv.Blog.Application.Dto;

namespace Meowv.Blog.Admin.Pages;

public partial class Index
{
    private List<NameValue> data = new();

    private bool isLoading = true;

    private Tuple<int, int, int> statistics = new(888, 888, 888);

    protected override async Task OnInitializedAsync()
    {
        var statisticsResponse = await GetResultAsync<BlogResponse<Tuple<int, int, int>>>("api/meowv/blog/statistics");
        if (statisticsResponse.Success)
            statistics = statisticsResponse.Result;
        else
            await Message.Error(statisticsResponse.Message);

        var healthResponse = await GetResultAsync<BlogResponse<List<NameValue>>>("api/meowv/health");
        if (healthResponse.Success)
        {
            data = healthResponse.Result;

            isLoading = false;
        }
        else
        {
            await Message.Error(healthResponse.Message);
        }
    }

    private class NameValue
    {
        public string Name { get; set; }

        public string Value { get; set; }
    }
}