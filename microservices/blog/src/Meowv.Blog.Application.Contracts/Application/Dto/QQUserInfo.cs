using System.Text.Json.Serialization;

namespace Meowv.Blog.Application.Dto;

public class QQUserInfo
{
    public string Id { get; set; }

    [JsonPropertyName("nickname")] public string Name { get; set; }

    [JsonPropertyName("figureurl_qq")] public string Avatar { get; set; }

    public string Email { get; set; } = "";
}

public class QQOpenId
{
    [JsonPropertyName("openid")] public string OpenId { get; set; }
}