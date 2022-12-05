﻿using System;
using System.Collections.Concurrent;
using System.ComponentModel.Composition;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using Emulator.PluginFramework.Internals;
using Emulator.Shell.Interfaces;

namespace Emulator.Shell.Applications.RemoteServices
{
    [Export(typeof(IEventAggregator))]
    public class EventAggregator : RemoteService, IEventAggregator
    {
        private readonly ConcurrentDictionary<Type, object> subjects = new ConcurrentDictionary<Type, object>();

        public IObservable<TEvent> GetEvent<TEvent>()
        {
            var subject = (ISubject<TEvent>)subjects.GetOrAdd(typeof(TEvent), t => new Subject<TEvent>());
            return subject.AsObservable().Remotable(null);
        }

        public void Publish<TEvent>(TEvent sampleEvent)
        {
            object subject;
            if (subjects.TryGetValue(typeof(TEvent), out subject))
            {
                try
                {
                    ((ISubject<TEvent>)subject).OnNext(sampleEvent);
                }
                catch (Exception)
                {
                    // This can happen when a Plugin did not unsubscribe from the event.
                }
            }
        }
    }
}
