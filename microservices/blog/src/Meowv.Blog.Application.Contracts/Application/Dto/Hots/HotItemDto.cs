namespace Meowv.Blog.Application.Dto.Hots
{
    public class HotItemDto<TResult>
    {
        public string Source { get; set; }

        public TResult Result { get; set; }
    }
}