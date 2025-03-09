using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Meowv.Blog.Application.Dto;

namespace Meowv.Blog.Admin.Pages.Tools;

public partial class Ip2Region
{
    private string ip;
    private string region;
    private string returnIp;

    protected override async Task OnInitializedAsync()
    {
        await OnSearch();
    }

    public async Task OnSearch()
    {
        var response = await GetResultAsync<BlogResponse<List<string>>>($"api/meowv/tool/ip2region?ip={ip}");
        if (!response.Success)
        {
            await Message.Error(response.Message);
        }
        else
        {
            var result = response.Result;
            returnIp = result.First();
            region = string.Join(" ", result.Skip(1));
        }
    }
}