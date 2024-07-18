using Services.SceneLoaders;
using Zenject;

namespace Infrastructure.Instalers
{
    public class LoadSceneInstaler : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<UnitySceneLoader>().AsSingle().NonLazy();
        }
    }
}
