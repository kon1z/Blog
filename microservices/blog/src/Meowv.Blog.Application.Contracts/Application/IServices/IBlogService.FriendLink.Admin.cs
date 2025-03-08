using Meowv.Blog.Application.Dto;
using Meowv.Blog.Application.Dto.Blog;
using Meowv.Blog.Application.Dto.Blog.Params;

namespace Meowv.Blog.Application.IServices
{
    public partial interface IBlogService
    {
        Task<BlogResponse> CreateFriendLinkAsync(CreateFriendLinkInput input);

        Task<BlogResponse> DeleteFriendLinkAsync(string id);

        Task<BlogResponse> UpdateFriendLinkAsync(string id, UpdateFriendLinkInput input);

        Task<BlogResponse<List<GetAdminFriendLinkDto>>> GetAdminFriendLinksAsync();
    }
}