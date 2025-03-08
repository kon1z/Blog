using Meowv.Blog.Application.Dto;
using Meowv.Blog.Application.Dto.Messages;
using Meowv.Blog.Application.Dto.Messages.Params;

namespace Meowv.Blog.Application.IServices
{
    public partial interface IMessageService
    {
        Task<BlogResponse> CreateAsync(CreateMessageInput input);

        Task<BlogResponse> ReplyAsync(string id, ReplyMessageInput input);

        Task<BlogResponse> DeleteAsync(string id);

        Task<BlogResponse> DeleteReplyAsync(string id, string replyId);

        Task<BlogResponse<PagedList<MessageDto>>> GetMessagesAsync(int page, int limit);
    }
}