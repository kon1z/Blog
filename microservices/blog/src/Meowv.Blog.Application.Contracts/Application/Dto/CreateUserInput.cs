﻿namespace Meowv.Blog.Application.Dto;

public class CreateUserInput
{
    public string Username { get; set; }

    public string Password { get; set; }

    public string Type { get; set; }

    public string Identity { get; set; }

    public string Name { get; set; }

    public string Avatar { get; set; }

    public string Email { get; set; }
}