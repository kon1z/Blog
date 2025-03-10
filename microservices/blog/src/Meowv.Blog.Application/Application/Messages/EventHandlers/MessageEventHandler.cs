using System.Threading.Tasks;
using Meowv.Blog.Domain.Messages;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Entities.Events;
using Volo.Abp.EventBus;

namespace Meowv.Blog.Application.Messages.EventHandlers;

public class MessageEventHandler : ILocalEventHandler<EntityCreatedEventData<Message>>,
    ITransientDependency
{
    public Task HandleEventAsync(EntityCreatedEventData<Message> eventData)
    {
        // TODO ToolAppService.SendMessage
        return Task.CompletedTask;
    }
}