using System;

namespace PoolObject
{
    public interface IPool<T>
    {
        public void Prewarm(int count);
        public T Request();
        public void Return(T member);
    }
}
