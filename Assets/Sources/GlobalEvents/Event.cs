using System;

namespace Sources.GlobalEvents
{
    public abstract class Event : EventBase<Action>
    {
        public void Invoke() =>
            _listeners.ForEach(listener => listener.Invoke());
    }

    public abstract class Event<T> : EventBase<Action<T>>
    {
        public void Invoke(T arg) =>
            _listeners.ForEach(listener => listener.Invoke(arg));
    }
    
    public abstract class Event<T1, T2> : EventBase<Action<T1, T2>>
    {
        public void Invoke(T1 arg1, T2 arg2) =>
            _listeners.ForEach(listener => listener.Invoke(arg1, arg2));
    }
    
    public abstract class Event<T1, T2, T3> : EventBase<Action<T1, T2, T3>>
    {
        public void Invoke(T1 arg1, T2 arg2, T3 arg3) =>
            _listeners.ForEach(listener => listener.Invoke(arg1, arg2, arg3));
    }
    
    public abstract class Event<T1, T2, T3, T4> : EventBase<Action<T1, T2, T3, T4>>
    {
        public void Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4) =>
            _listeners.ForEach(listener => listener.Invoke(arg1, arg2, arg3, arg4));
    }
    
    public abstract class Event<T1, T2, T3, T4, T5> : EventBase<Action<T1, T2, T3, T4, T5>>
    {
        public void Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5) =>
            _listeners.ForEach(listener => listener.Invoke(arg1, arg2, arg3, arg4, arg5));
    }
}