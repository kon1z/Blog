namespace Meowv.Blog.Application.Dto;

public interface IListResult<T>
{
    IReadOnlyList<T> Item { get; set; }
}