namespace Meowv.Blog.Options.Authorize;

public class WeiboOptions
{
    public string AccessTokenUrl = "https://api.weibo.com/oauth2/access_token";

    public string AuthorizeUrl = "https://api.weibo.com/oauth2/authorize";

    public string UserInfoUrl = "https://api.weibo.com/2/users/show.json";
    public string ClientId { get; set; }

    public string ClientSecret { get; set; }

    public string RedirectUrl { get; set; }

    public string Scope { get; set; }
}