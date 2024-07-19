using ReactivePropertes;
using UnityEngine;

namespace Shooting.Weapons
{
    public abstract class ReloadSystem : MonoBehaviour, IReloadeble
    {
        protected IReactiveProperty<int> NumberCartridges;

        public void Init(IReactiveProperty<int> numberCartridges)
        {
            NumberCartridges = numberCartridges;
        }

        public abstract void Reload();
    }
}
