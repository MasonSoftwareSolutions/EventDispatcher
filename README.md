# Event Dispatcher
This event dispatcher allows you to register event handlers and deferred event handlers. Regular event handlers handle events upon dispatch, deferred event handlers handle previously dispatched events once `Deferred()` has been invoked. The use case this was built for is dispatching events which can handled before and after entities are commited to the database.

Event dispatcher requires the use of [Microsoft.Extensions.DependencyInjection](https://www.nuget.org/packages/Microsoft.Extensions.DependencyInjection/) for handler discovery. The library provides a service collection extension for easy dependency registration.

## Code Sample
```c#
// Dispatch your events
foreach (var @event in events)
{
    await _eventDispatcher.Dispatch(@event, cancellationToken);
}

// Commit changes to your database (Entity Framework example)
await _db.SaveChangesAsync(cancellationToken);

// Tell the event dispatcher to replay dispatched events for deferred event handlers
await _eventDispatcher.Deferred(cancellationToken);
```