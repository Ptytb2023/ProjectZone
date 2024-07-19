namespace Pool
{
    public interface IPool<T>
    {
        public T Request();
        public void Return(T member);
    }
}
