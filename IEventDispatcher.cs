using System.Threading;
using System.Threading.Tasks;

namespace MasonSoftwareSolutions.EventDispatcher
{
    public interface IEventDispatcher
    {
        Task Deferred(CancellationToken cancellationToken = default);
        Task Dispatch<TEvent>(TEvent @event, CancellationToken cancellationToken = default) where TEvent : IEvent;
    }
}
