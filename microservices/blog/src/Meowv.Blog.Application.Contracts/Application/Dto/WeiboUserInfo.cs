using System.Text.Json.Serialization;

namespace Meowv.Blog.Application.Dto;

public class WeiboUserInfo
{
    [JsonPropertyName("idstr")] public string Id { get; set; }

    [JsonPropertyName("screen_name")] public string Login { get; set; }

    [JsonPropertyName("name")] public string Name { get; set; }

    [JsonPropertyName("avatar_large")] public string Avatar { get; set; }

    public string Email { get; set; } = "";
}