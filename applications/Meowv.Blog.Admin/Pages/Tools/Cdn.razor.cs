using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Meowv.Blog.Application.Dto;

namespace Meowv.Blog.Admin.Pages.Tools;

public partial class Cdn
{
    private string type = "1", urls1, urls2, urls3;

    private void OnTabChange(string key)
    {
        type = key;
    }

    public async Task SubmitAsync()
    {
        var urls = Array.Empty<string>();
        var api = string.Empty;

        switch (type)
        {
            case "1":
            {
                if (string.IsNullOrWhiteSpace(urls1))
                {
                    await Message.Error("请输入URL");
                    return;
                }

                urls = urls1.Split("\n");
                api = "api/meowv/tool/cdn/purge/url";
                break;
            }
            case "2":
            {
                if (string.IsNullOrWhiteSpace(urls2))
                {
                    await Message.Error("请输入URL");
                    return;
                }

                urls = urls2.Split("\n");
                api = "api/meowv/tool/cdn/purge/path";
                break;
            }
            case "3":
            {
                if (string.IsNullOrWhiteSpace(urls3))
                {
                    await Message.Error("请输入URL");
                    return;
                }

                urls = urls3.Split("\n");
                api = "api/meowv/tool/cdn/push/url";
                break;
            }
        }

        var json = JsonSerializer.Serialize(urls);

        var response = await GetResultAsync<BlogResponse<dynamic>>(api, json, HttpMethod.Post);
        if (response.Success)
        {
            urls1 = urls2 = urls3 = "";
            await Message.Success("Successful");
        }
        else
        {
            await Message.Error(response.Message);
        }
    }
}