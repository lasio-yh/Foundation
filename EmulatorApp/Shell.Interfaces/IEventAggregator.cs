using System;

namespace Emulator.Shell.Interfaces
{
    public interface IEventAggregator
    {
        void Publish<TEvent>(TEvent sampleEvent);

        IObservable<TEvent> GetEvent<TEvent>();
    }
}
