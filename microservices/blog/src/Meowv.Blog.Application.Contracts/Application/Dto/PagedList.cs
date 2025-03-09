namespace Meowv.Blog.Application.Dto;

public class PagedList<T> : ListResult<T>, IPagedList<T>
{
    public PagedList(int total, IReadOnlyList<T> result) : base(result)
    {
        Total = total;
    }

    public int Total { get; set; }
}