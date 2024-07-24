using Shooting.Settings;
using UnityEngine;

namespace Shooting.Weapons
{
    public abstract class BaseWeapon : MonoBehaviour, IWeapon
    {
        [field: SerializeField] public WeaponSettings Settings { get; private set; }

        public abstract void TryShoot();
    }
}
