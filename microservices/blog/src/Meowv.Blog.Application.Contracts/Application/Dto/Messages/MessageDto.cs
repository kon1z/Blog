namespace Meowv.Blog.Application.Dto.Messages
{
    public class MessageDto : MessageReplyDto
    {
        public List<MessageReplyDto> Reply { get; set; }
    }
}