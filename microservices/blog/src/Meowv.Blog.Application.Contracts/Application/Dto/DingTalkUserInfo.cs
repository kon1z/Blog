using System.Text.Json.Serialization;

namespace Meowv.Blog.Application.Dto;

public class DingTalkUserInfo
{
    [JsonPropertyName("user_info")] public DingTalkUserInfoResponse UserInfoResponse { get; set; }

    [JsonPropertyName("errmsg")] public string ErrMsg { get; set; }

    [JsonPropertyName("errcode")] public int ErrCode { get; set; }
}

public class DingTalkUserInfoResponse
{
    [JsonPropertyName("unionid")] public string Id { get; set; }

    [JsonPropertyName("nick")] public string Name { get; set; }

    public string Avatar { get; set; } = "";

    public string Email { get; set; } = "";
}