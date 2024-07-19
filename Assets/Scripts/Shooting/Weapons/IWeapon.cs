using ReactivePropertes;
using System.Collections;

namespace Shooting.Weapons
{
    public interface IWeapon : IReloadeble
    {
        public IEnumerator Shoot();
    }

    public interface IReloadeble
    {
        public IObservable<int> Ammo { get; }
        public IEnumerator Reload();
    }
}
