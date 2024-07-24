using System;

public interface IZoneTrigger<T>
{
    event Action<T> TrigerEnter;

    void SetRadius(float radius);
}