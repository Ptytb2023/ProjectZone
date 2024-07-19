using Assets.ReactivePropertes;
using DataPersistence;
using ReactivePropertes;
using System.Collections;
using UnityEngine;

namespace Shooting.Weapons
{
    public abstract class BaseWeapon : MonoBehaviour, IWeapon
    {
        [SerializeField] protected Transform PointShoot;
        [SerializeField] protected WeaponData WeaponData;

        protected IReactiveProperty<int> CurrentAmmo = new ReactiveProperty<int>();

        public IObservable<int> Ammo => CurrentAmmo;

        public abstract IEnumerator Reload();

        public abstract IEnumerator Shoot();
        
    }
}
