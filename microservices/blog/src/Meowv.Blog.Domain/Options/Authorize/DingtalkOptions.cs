namespace Meowv.Blog.Options.Authorize;

public class DingtalkOptions
{
    public string AuthorizeUrl = "https://oapi.dingtalk.com/connect/qrconnect";

    public string UserInfoUrl = "https://oapi.dingtalk.com/sns/getuserinfo_bycode";
    public string AppId { get; set; }

    public string AppSecret { get; set; }

    public string RedirectUrl { get; set; }

    public string Scope { get; set; }
}