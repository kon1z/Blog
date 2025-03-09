using System.Text.Json.Serialization;

namespace Meowv.Blog.Application.Dto;

public class AlipayUserInfo
{
    [JsonPropertyName("sign")] public string Sign { get; set; }

    [JsonPropertyName("alipay_user_info_share_response")]
    public AlipayUserInfoResponse UserInfoResponse { get; set; }
}

public class AlipayUserInfoResponse
{
    [JsonPropertyName("user_id")] public string Id { get; set; }

    [JsonPropertyName("nick_name")] public string Name { get; set; }

    [JsonPropertyName("avatar")] public string Avatar { get; set; }

    public string Email { get; set; } = "";
}