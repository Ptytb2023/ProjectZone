using System;
using UnityEngine;

public interface ITriggerObserver
{
    event Action<Collider2D> TrigerEnter;
    event Action<Collider2D> TrigerExit;

    void SetRadius(float radius);
} 