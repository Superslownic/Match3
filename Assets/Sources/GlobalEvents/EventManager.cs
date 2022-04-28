using System;
using System.Collections.Generic;

namespace Sources.GlobalEvents
{
    public static class EventManager
    {
        private static readonly Dictionary<Type, IGlobalEvent> _events =
            new Dictionary<Type, IGlobalEvent>();

        public static T GetEvent<T>() where T : IGlobalEvent
        {
            try
            {
                return (T) _events[typeof(T)];
            }
            catch (KeyNotFoundException)
            {
                T instance = Activator.CreateInstance<T>();
                _events.Add(typeof(T), instance);
                return instance;
            }
        }
    }
}