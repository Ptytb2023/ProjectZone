using System;

namespace ReactivePropertes
{
    public interface IReactiveProperty<T> : IReadOnlyReactiveProperty<T>
    {
        T Value { get; set; }
    }

    public interface IReadOnlyReactiveProperty<T> : IObservable<T>
    {
        T GetValue();
    }

    public interface IObservable<T>
    {
        void Subscribe(Action<T> subscriber);
        void SubscribeAndUpdate(Action<T> subscriber);
        void Unsubscribe(Action<T> subscriber);
    }
}
