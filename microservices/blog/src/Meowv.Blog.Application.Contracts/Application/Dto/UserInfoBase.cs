using System.Text.Json.Serialization;

namespace Meowv.Blog.Application.Dto;

public class UserInfoBase
{
    [JsonPropertyName("id")] public string Id { get; set; }

    [JsonPropertyName("login")] public string Login { get; set; }

    [JsonPropertyName("name")] public string Name { get; set; }

    [JsonPropertyName("avatar_url")] public string Avatar { get; set; }

    [JsonPropertyName("email")] public string Email { get; set; }
}