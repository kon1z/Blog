using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Meowv.Blog.Web.Pages;

public abstract class PageBase : PageModel
{
    private readonly HttpClient http;

    public PageBase(IHttpClientFactory httpClientFactory)
    {
        http = httpClientFactory.CreateClient("api");
    }

    public virtual int PageSize { get; set; } = 15;

    public virtual async Task<T> GetResultAsync<T>(string url)
    {
        var json = await http.GetStringAsync(url);
        return JsonSerializer.Deserialize<T>(json);
    }
}