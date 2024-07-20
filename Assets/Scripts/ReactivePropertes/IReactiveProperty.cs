using System;

namespace ReactivePropertes
{
    public interface IReactiveProperty<T> : IReadOnlyReactiveProperty<T>
    {
        public T Value { get; set; }
    }

    public interface IReadOnlyReactiveProperty<T> : IObservable<T>
    {
        public T GetCurrentValue();
    }

    public interface IObservable<T>
    {
        public void Subscribe(Action<T> subscriber);
        public void SubscribeAndUpdate(Action<T> subscriber);
        public void Unsubscribe(Action<T> subscriber);
    }
}
