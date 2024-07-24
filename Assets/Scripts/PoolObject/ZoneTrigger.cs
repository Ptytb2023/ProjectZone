using System;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class ZoneTrigger<T> : MonoBehaviour, IZoneTrigger<T>
{
    public event Action<T> TrigerEnter;

    private CircleCollider2D _circleCollider;

    private void Awake()
    {
        _circleCollider = GetComponent<CircleCollider2D>();
        _circleCollider.isTrigger = true;
    }

    public void SetRadius(float radius) =>
        _circleCollider.radius = radius;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out T component))
            TrigerEnter?.Invoke(component);
    }
}


