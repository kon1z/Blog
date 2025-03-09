namespace Meowv.Blog.Application.Dto;

public class ListResult<T> : IListResult<T>
{
    private IReadOnlyList<T> item;

    public ListResult(IReadOnlyList<T> item)
    {
        Item = item;
    }

    public IReadOnlyList<T> Item
    {
        get => item ??= new List<T>();
        set => item = value;
    }
}