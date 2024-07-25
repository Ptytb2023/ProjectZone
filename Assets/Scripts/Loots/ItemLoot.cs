using System;
using DG.Tweening;
using ItemSystem;
using PoolObject;
using UnityEngine;

using Random = UnityEngine.Random;

namespace Loots
{
    [RequireComponent(typeof(CircleCollider2D), typeof(SpriteRenderer))]
    public class ItemLoot : MonoBehaviour, IItemLoot, IPoolabel
    {
        [SerializeField] private float _minDropDistance;
        [SerializeField] private float _maxDropDistance;
        [SerializeField] private Ease _dropEase;

        [SerializeField] private float _dropDuration;
        [SerializeField] private float _trigerRadius;

        private CircleCollider2D _collider;
        private SpriteRenderer _spriteRenderer;
        private Tween _dropAnimation;

        public IItem Loot { get; private set; }
        public Action<IItemLoot> PickUp;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _collider = GetComponent<CircleCollider2D>();

            ResetParametrs();
        }

        public void AssignItem(IItem item)
        {
            Loot = item;

            _spriteRenderer.sprite = Loot.Icon;
            float radius = GetRadius();

            _collider.radius = radius;
        }

        public void OnSpawn() =>
           _spriteRenderer.enabled = true;

        public void OnDisapw()
        {
            if (_dropAnimation is not null)
                _dropAnimation.Kill();

            ResetParametrs(); 
        }

        public void Drop(Vector2 from, Vector2 direction)
        {
            transform.position = from;

            Vector2 to = CalculateDestination(direction)+ from;
            

            _dropAnimation =
                transform.DOMove(to, _dropDuration).
                SetEase(_dropEase).
                OnComplete(OnEndDrop);
        }

        private Vector2 CalculateDestination(Vector2 direction)
        {
            float addet = Random.Range(_minDropDistance, _maxDropDistance);
            return direction * addet;
        }

        private float GetRadius()
        {
            Vector3 size = _spriteRenderer.bounds.size;
            float radius = size.x / 2f * Mathf.Sqrt(2);
            return radius;
        }

        private void OnEndDrop() =>
            _collider.enabled = true;

        private void ResetParametrs()
        {
            _collider.enabled = false;
            _spriteRenderer.sprite = null;
            _spriteRenderer.enabled = false;
        }
    }
}