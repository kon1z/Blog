using System.Threading.Tasks;
using Meowv.Blog.Domain.Signatures;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Entities.Events;
using Volo.Abp.EventBus;

namespace Meowv.Blog.Application.Signatures.EventHandlers;

public class SignatureEventHandler : ILocalEventHandler<EntityCreatedEventData<Signature>>,
    ILocalEventHandler<EntityDeletedEventData<Signature>>,
    ITransientDependency
{
    public Task HandleEventAsync(EntityCreatedEventData<Signature> eventData)
    {
        return Task.CompletedTask;
    }

    public Task HandleEventAsync(EntityDeletedEventData<Signature> eventData)
    {
        return Task.CompletedTask;
    }
}