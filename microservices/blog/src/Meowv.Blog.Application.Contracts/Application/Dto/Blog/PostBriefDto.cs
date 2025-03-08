using System.Text.Json.Serialization;

namespace Meowv.Blog.Application.Dto.Blog
{
    public class PostBriefDto
    {
        public string Title { get; set; }

        public string Url { get; set; }

        [JsonIgnore]
        public int Year { get; set; }

        public string CreatedAt { get; set; }
    }
}