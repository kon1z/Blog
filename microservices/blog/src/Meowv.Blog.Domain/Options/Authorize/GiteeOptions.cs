namespace Meowv.Blog.Options.Authorize;

public class GiteeOptions
{
    public string AccessTokenUrl = "https://gitee.com/oauth/token";

    public string AuthorizeUrl = "https://gitee.com/oauth/authorize";

    public string UserInfoUrl = "https://gitee.com/api/v5/user";
    public string ClientId { get; set; }

    public string ClientSecret { get; set; }

    public string RedirectUrl { get; set; }

    public string Scope { get; set; }
}