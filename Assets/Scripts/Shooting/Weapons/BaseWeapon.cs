using Shooting.Settings;
using UnityEngine;

namespace Shooting.Weapons
{
    [RequireComponent(typeof(SpriteRenderer))]
    public abstract class BaseWeapon : MonoBehaviour, IWeapon
    {
        [field: SerializeField] public WeaponSettings Settings { get; private set; }

        [SerializeField] private SpriteRenderer _spriteRenderer;

        private void Update()
        {
            if (transform.eulerAngles.z > 90 || transform.eulerAngles.z < -90)
                _spriteRenderer.flipY = true;
            else
                _spriteRenderer.flipY = false;
        }

        public abstract void TryShoot();
    }
}
