namespace Meowv.Blog.Application.Dto;

public class PostPagedDto
{
    public PostPagedDto()
    {
    }

    public PostPagedDto(string title, string url)
    {
        Title = title;
        Url = url;
    }

    public string Title { get; set; }

    public string Url { get; set; }
}