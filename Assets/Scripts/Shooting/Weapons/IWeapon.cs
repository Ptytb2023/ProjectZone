using Inventarys;
using ReactivePropertes;
using Shooting.Settings;

namespace Shooting.Weapons
{
    public interface IWeapon
    {
        WeaponSettings Settings { get; }
        void TryShoot();
    }

    public interface IReloadable
    {
        IReadOnlyReactiveProperty<int> Ammo { get; }
        void Reload();
    }

    public interface IGun : IReloadable, IWeapon
    {
    }
}
