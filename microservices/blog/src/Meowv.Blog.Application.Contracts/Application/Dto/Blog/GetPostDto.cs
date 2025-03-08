namespace Meowv.Blog.Application.Dto.Blog
{
    public class GetPostDto
    {
        public int Year { get; set; }

        public IEnumerable<PostBriefDto> Posts { get; set; }
    }
}