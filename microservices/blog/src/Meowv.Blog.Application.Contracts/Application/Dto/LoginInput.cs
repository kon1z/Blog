﻿namespace Meowv.Blog.Application.Dto;

public class LoginInput
{
    public string Type { get; set; } = "account";

    public string Username { get; set; }

    public string Password { get; set; }

    public string Code { get; set; }
}