using System;
using System.Collections.Generic;

namespace Sources.Services
{
    public sealed class ServiceLocator
    {
        private readonly Dictionary<Type, object> _container =
            new Dictionary<Type, object>();

        public void Register<T>(T service) =>
            _container.Add(typeof(T), service);

        public T Resolve<T>() =>
            (T)_container[typeof(T)];
    }
}