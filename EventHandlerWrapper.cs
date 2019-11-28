using System;
using System.Threading;
using System.Threading.Tasks;

namespace MasonSoftwareSolutions.EventDispatcher
{
    internal abstract class EventHandlerWrapper
    {
        public abstract Task Handle(IEvent @event, IServiceProvider serviceProvider, CancellationToken cancellationToken = default);
    }
}
