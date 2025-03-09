namespace Meowv.Blog.Application.Dto;

public class MessageDto : MessageReplyDto
{
    public List<MessageReplyDto> Reply { get; set; }
}