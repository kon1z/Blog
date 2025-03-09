using System.Text.Json.Serialization;

namespace Meowv.Blog.Application.Dto;

public class WeiboAccessToken
{
    [JsonPropertyName("access_token")] public string AccessToken { get; set; }

    [JsonPropertyName("uid")] public virtual string Uid { get; set; }
}