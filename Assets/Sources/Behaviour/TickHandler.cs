using System;
using System.Collections.Generic;
using Sources.Singletons;
using UnityEngine;

namespace Sources.Behaviour
{
    public class TickHandler : MonoSingleton<TickHandler>
    {
        private readonly List<IUpdateListener> _updateListeners = new List<IUpdateListener>();

        public void AddListener(IUpdateListener listener)
        {
            if(!_updateListeners.Contains(listener))
                _updateListeners.Add(listener);
        }
        
        public void RemoveListener(IUpdateListener listener)
        {
            if(_updateListeners.Contains(listener))
                _updateListeners.Remove(listener);
        }

        private void Update()
        {
            foreach (IUpdateListener listener in _updateListeners)
                listener.Update(Time.deltaTime);
        }
    }
}