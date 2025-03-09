namespace Meowv.Blog.Application.Dto;

public class GetPostDto
{
    public int Year { get; set; }

    public IEnumerable<PostBriefDto> Posts { get; set; }
}