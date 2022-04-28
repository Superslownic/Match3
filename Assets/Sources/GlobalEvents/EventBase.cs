using System.Collections.Generic;

namespace Sources.GlobalEvents
{
    public abstract class EventBase<TListener> : IGlobalEvent
    {
        protected readonly List<TListener> _listeners = new List<TListener>();
        
        public void AddListener(TListener listener)
        {
            if(_listeners.Contains(listener))
                return;
            
            _listeners.Add(listener);
        }
        
        public void RemoveListener(TListener listener)
        {
            if(!_listeners.Contains(listener))
                return;
            
            _listeners.Add(listener);
        }
    }
}