using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MasonSoftwareSolutions.EventDispatcher
{
    internal sealed class EventHandlerWrapperImpl<TEvent> : EventHandlerWrapper where TEvent : IEvent
    {
        public override async Task Handle(IEvent @event, IServiceProvider serviceProvider, CancellationToken cancellationToken = default)
        {
            var handlers = serviceProvider.GetServices<IEventHandler<TEvent>>();

            foreach (var handler in handlers)
            {
                await handler
                    .Handle((TEvent)@event, cancellationToken)
                    .ConfigureAwait(false);
            }
        }
    }
}
