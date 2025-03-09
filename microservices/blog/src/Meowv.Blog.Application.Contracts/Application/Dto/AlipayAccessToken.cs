using System.Text.Json.Serialization;

namespace Meowv.Blog.Application.Dto;

public class AlipayAccessToken
{
    [JsonPropertyName("sign")] public string Sign { get; set; }

    [JsonPropertyName("alipay_system_oauth_token_response")]
    public AlipayAccessTokenResponse AccessTokenResponse { get; set; }
}

public class AlipayAccessTokenResponse
{
    [JsonPropertyName("access_token")] public string AccessToken { get; set; }

    [JsonPropertyName("alipay_user_id")] public string AlipayUserId { get; set; }

    [JsonPropertyName("expires_in")] public int ExpiresIn { get; set; }

    [JsonPropertyName("re_expires_in")] public int ReExpiresIn { get; set; }

    [JsonPropertyName("refresh_token")] public string RefreshToken { get; set; }

    [JsonPropertyName("user_id")] public string UserId { get; set; }
}