﻿using System.Text.Json.Serialization;

namespace Meowv.Blog.Application.Dto;

public class AccessTokenBase
{
    [JsonPropertyName("access_token")] public string AccessToken { get; set; }

    [JsonPropertyName("scope")] public string Scope { get; set; }

    [JsonPropertyName("token_type")] public string TokenType { get; set; }
}