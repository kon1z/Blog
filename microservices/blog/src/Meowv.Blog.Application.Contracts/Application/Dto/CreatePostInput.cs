﻿namespace Meowv.Blog.Application.Dto;

public class CreatePostInput
{
    public string Title { get; set; }

    public string Author { get; set; }

    public string Url { get; set; }

    public string Markdown { get; set; }

    public string CategoryId { get; set; }

    public List<string> Tags { get; set; } = new();

    public string CreatedAt { get; set; }
}