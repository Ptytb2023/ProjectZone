using ReactivePropertes;
using Shooting.Settings;

namespace Shooting.Weapons
{
    public interface IWeapon
    {
        public WeaponSettings Settings { get; }
        void Shoot();
        void SetActive(bool value);
    }

    public interface IReloadable
    {
        bool IsReloading { get; }
        IObservable<int> AmmoChanged { get; }
        void Reload();
    }

    public interface IGun : IWeapon, IReloadable
    {
    }
}
