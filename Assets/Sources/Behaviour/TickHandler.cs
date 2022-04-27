using System;
using System.Collections.Generic;
using Sources.Singletons;
using UnityEngine;

namespace Sources.Behaviour
{
    public class TickHandler : MonoSingleton<TickHandler>
    {
        private readonly List<ITickListener> _tickListeners = new List<ITickListener>();
        private readonly List<IFixedTickListener> _fixedTickListeners = new List<IFixedTickListener>();

        public void AddListener(ITickListener listener)
        {
            if(!_tickListeners.Contains(listener))
                _tickListeners.Add(listener);
        }
        
        public void AddListener(IFixedTickListener listener)
        {
            if(!_fixedTickListeners.Contains(listener))
                _fixedTickListeners.Add(listener);
        }
        
        public void RemoveListener(ITickListener listener)
        {
            if(_tickListeners.Contains(listener))
                _tickListeners.Remove(listener);
        }
        
        public void RemoveListener(IFixedTickListener listener)
        {
            if(_fixedTickListeners.Contains(listener))
                _fixedTickListeners.Remove(listener);
        }

        private void Update()
        {
            foreach (ITickListener listener in _tickListeners)
                listener.Tick(Time.deltaTime);
            
            foreach (IFixedTickListener listener in _fixedTickListeners)
                listener.FixedTick(Time.fixedDeltaTime);
        }
    }
}