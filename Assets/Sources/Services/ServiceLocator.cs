using System;
using System.Collections.Generic;

namespace Sources.Services
{
    public sealed class ServiceLocator
    {
        private readonly Dictionary<Type, object> _container =
            new Dictionary<Type, object>();

        public T Resolve<T>() =>
            (T)_container[typeof(T)];

        public void Register<T>(T service) =>
            _container.Add(typeof(T), service);

        public Bindable<T> Register<T>() =>
            new Bindable<T>(_container);
    }

    public class Bindable<T>
    {
        private readonly Dictionary<Type, object> _container;

        public Bindable(Dictionary<Type, object> container) =>
            _container = container;

        public void FromInstance(T instance) =>
            _container.Add(typeof(T), instance);
    }
}