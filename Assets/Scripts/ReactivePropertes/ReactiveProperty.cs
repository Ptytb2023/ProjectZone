using System;
using System.Collections.Generic;

namespace ReactivePropertes
{
    public class ReactiveProperty<T> : IReactiveProperty<T>
    {
        private T _currentValue;

        private List<Action<T>> _subscribers = new List<Action<T>>();

        public T Value
        {
            get => _currentValue;
            set
            {
                _currentValue = value;
                NotifySubscribers();
            }
        }

        public T GetValue() =>
            Value;

        public void Subscribe(Action<T> handler) =>
            _subscribers.Add(handler);

        public void SubscribeAndUpdate(Action<T> handler)
        {
            _subscribers.Add(handler);
            handler?.Invoke(Value);
        }

        public void UnSubscribe(Action<T> handler) =>
            _subscribers.Remove(handler);

        private void NotifySubscribers() =>
            _subscribers.ForEach(subscribe => subscribe?.Invoke(Value));
    }
}
