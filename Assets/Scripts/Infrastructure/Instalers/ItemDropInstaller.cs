using Loots;
using Zenject;

namespace Infrastructure.Instalers
{
    public class ItemDropInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<ItemDropper>().AsSingle().NonLazy();
        }
    }
}
