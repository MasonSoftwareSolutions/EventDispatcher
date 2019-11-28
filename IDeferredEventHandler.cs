using System.Threading;
using System.Threading.Tasks;

namespace MasonSoftwareSolutions.EventDispatcher
{
    public interface IDeferredEventHandler<TEvent> where TEvent : IEvent
    {
        Task Handle(TEvent @event, CancellationToken cancellationToken = default);
    }
}
