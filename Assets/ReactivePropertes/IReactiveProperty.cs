using System;

namespace ReactivePropertes
{
    public interface IReactiveProperty<T> : IReadOnlyReactiveProperty<T>
    {
        public T Value { get; set; }
    }

    public interface IReadOnlyReactiveProperty<T> : IObservable<T>
    {
        public T GetValue();
    }

    public interface IObservable<T>
    {
        public void Subscribe(Action<T> handler);
        public void SubscribeAndUpdate(Action<T> handler);

        public void UnSubscribe(Action<T> handler);
    }
}
