using System;
using System.Collections.Generic;

namespace ReactivePropertes
{
    public class ReactiveProperty<T> : IReactiveProperty<T>
    {
        private T _currentValue;

        private List<Action<T>> _subscribers = new List<Action<T>>();

        public ReactiveProperty() =>
            Value = default(T);

        public ReactiveProperty(T value) =>
            Value = value;

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

        public void Subscribe(Action<T> subscriber) =>
            _subscribers.Add(subscriber);

        public void SubscribeAndUpdate(Action<T> subscriber)
        {
            _subscribers.Add(subscriber);
            subscriber?.Invoke(Value);
        }

        public void Unsubscribe(Action<T> subscriber) =>
            _subscribers.Remove(subscriber);

        private void NotifySubscribers() =>
            _subscribers.ForEach(subscriber => subscriber?.Invoke(Value));
    }
}
