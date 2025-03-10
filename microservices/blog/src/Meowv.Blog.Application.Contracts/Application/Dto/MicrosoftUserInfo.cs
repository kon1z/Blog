using System.Text.Json.Serialization;

namespace Meowv.Blog.Application.Dto;

public class MicrosoftUserInfo
{
    [JsonPropertyName("id")] public string Id { get; set; }

    [JsonPropertyName("displayName")] public string Name { get; set; }

    public string Avatar { get; set; } = "";

    [JsonPropertyName("userPrincipalName")]
    public string Email { get; set; }
}