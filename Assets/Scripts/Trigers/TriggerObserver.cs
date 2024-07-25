using System;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class TriggerObserver : MonoBehaviour, ITriggerObserver
{
    private CircleCollider2D _circleCollider;

    public event Action<Collider2D> TrigerExit;
    public event Action<Collider2D> TrigerEnter;

    private void Awake()
    {
        _circleCollider = GetComponent<CircleCollider2D>();
        _circleCollider.isTrigger = true;
    }

    public void SetRadius(float radius) =>
        _circleCollider.radius = radius;

    private void OnTriggerEnter2D(Collider2D collision) => 
        TrigerEnter?.Invoke(collision);

    private void OnTriggerExit2D(Collider2D collision) => 
        TrigerExit?.Invoke(collision);
}


