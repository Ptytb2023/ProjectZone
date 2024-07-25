using Inventarys;
using ReactivePropertes;
using Shooting.Settings;
using System;

namespace Shooting.Weapons
{
    public interface IWeapon
    {
        WeaponSettings Settings { get; }
        void TryShoot();
    }

    public interface IReloadable
    {
        event Action<float> ReloadStart;
        IReadOnlyReactiveProperty<int> Ammo { get; }
        void Reload();

        public class Empty : IReloadable
        {
            public IReadOnlyReactiveProperty<int> Ammo => null;

            public event Action<float> ReloadStart;

            public void Reload() => 
                ReloadStart?.Invoke(0);
        }
    }

    public interface IGun : IReloadable, IWeapon
    {
    }
}
