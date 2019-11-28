using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MasonSoftwareSolutions.EventDispatcher
{
    public sealed class EventDispatcher : IEventDispatcher
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly Queue<IEvent> _dispatchedEvents = new Queue<IEvent>();

        public EventDispatcher(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task Deferred(CancellationToken cancellationToken = default)
        {
            while (_dispatchedEvents.Count > 0)
            {
                var @event = _dispatchedEvents.Dequeue();
                var eventType = @event.GetType();

                var handler = (EventHandlerWrapper)Activator
                    .CreateInstance(typeof(DeferredEventHandlerWrapperImpl<>)
                    .MakeGenericType(eventType));
                await handler
                    .Handle(@event, _serviceProvider, cancellationToken)
                    .ConfigureAwait(false);
            }
        }

        public async Task Dispatch<TEvent>(TEvent @event, CancellationToken cancellationToken = default) where TEvent: IEvent
        {
            var handler = (EventHandlerWrapper)Activator
                .CreateInstance(typeof(EventHandlerWrapperImpl<>)
                .MakeGenericType(@event.GetType()));
            await handler
                .Handle(@event, _serviceProvider, cancellationToken)
                .ConfigureAwait(false);

            _dispatchedEvents.Enqueue(@event);
        }
    }
}
