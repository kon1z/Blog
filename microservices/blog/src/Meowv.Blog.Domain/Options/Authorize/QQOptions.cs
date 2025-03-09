namespace Meowv.Blog.Options.Authorize;

public class QQOptions
{
    public string AccessTokenUrl = "https://graph.qq.com/oauth2.0/token";

    public string AuthorizeUrl = "https://graph.qq.com/oauth2.0/authorize";

    public string OpenIdUrl = "https://graph.qq.com/oauth2.0/me";

    public string UserInfoUrl = "https://graph.qq.com/user/get_user_info";
    public string ClientId { get; set; }

    public string ClientSecret { get; set; }

    public string RedirectUrl { get; set; }

    public string Scope { get; set; }
}