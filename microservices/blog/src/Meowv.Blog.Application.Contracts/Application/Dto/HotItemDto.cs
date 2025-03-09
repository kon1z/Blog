namespace Meowv.Blog.Application.Dto;

public class HotItemDto<TResult>
{
    public string Source { get; set; }

    public TResult Result { get; set; }
}