using ReactivePropertes;

namespace Shooting.Weapons
{
    public interface IWeapon
    {
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
