using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Meowv.Blog.Application.Dto;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;

namespace Meowv.Blog.Admin.Services;

public class OAuthService : AuthenticationStateProvider
{
    private readonly IJSRuntime _jsRuntime;
    private readonly NavigationManager _navigationManager;
    private readonly HttpClient http;

    public OAuthService(IHttpClientFactory httpClientFactory, IJSRuntime jsRuntime, NavigationManager navigationManager)
    {
        http = httpClientFactory.CreateClient("api");
        _jsRuntime = jsRuntime;
        _navigationManager = navigationManager;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var token = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "token");

        if (string.IsNullOrEmpty(token)) return GetNullState();

        http.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
        var httpResponseMessage = await http.GetAsync("api/meowv/user");
        if (!httpResponseMessage.IsSuccessStatusCode) return GetNullState();

        var response = await httpResponseMessage.Content.ReadAsStringAsync();

        var user = JsonSerializer.Deserialize<BlogResponse<UserDto>>(response).Result;
        if (user is null) return GetNullState();

        var identity = new ClaimsIdentity(new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Name, user.Name),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim("avatar", user.Avatar)
        }, "meowv.blog oauth");

        var principal = new ClaimsPrincipal(identity);

        return new AuthenticationState(principal);
    }

    public async Task GetOAuthUrl(string type)
    {
        var json = await http.GetStringAsync($"/api/meowv/oauth/{type}");
        var response = JsonSerializer.Deserialize<BlogResponse<string>>(json);

        _navigationManager.NavigateTo(response.Success ? response.Result : "/login");
    }

    public async Task<string> GetTokenAsync(LoginInput login)
    {
        HttpResponseMessage httpResponse;

        if (login.Type == "account")
        {
            var json = JsonSerializer.Serialize(new
            {
                username = login.Username,
                password = login.Password
            });
            var content = new ByteArrayContent(Encoding.UTF8.GetBytes(json));
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            httpResponse = await http.PostAsync("api/meowv/oauth/account/token", content);
        }
        else
        {
            httpResponse = await http.PostAsync($"api/meowv/oauth/token?code={login.Code}", null);
        }

        var response = await httpResponse.Content.ReadAsStringAsync();
        var token = JsonSerializer.Deserialize<BlogResponse<string>>(response).Result;
        await _jsRuntime.InvokeVoidAsync("localStorage.setItem", "token", token);

        return token;
    }

    public async Task LogoutAsync()
    {
        await _jsRuntime.InvokeVoidAsync("localStorage.setItem", "token", "");

        NotifyAuthenticationStateChanged(Task.FromResult(GetNullState()));
    }

    private AuthenticationState GetNullState()
    {
        _navigationManager.NavigateTo("/login");

        var principal = new ClaimsPrincipal(new ClaimsIdentity());
        return new AuthenticationState(principal);
    }
}