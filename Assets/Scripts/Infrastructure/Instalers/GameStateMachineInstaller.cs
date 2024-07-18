using Infrastructure.FSMGame;
using Zenject;

namespace Infrastructure.Instalers
{
    public class GameStateMachineInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<GameStateMachine>().AsSingle().NonLazy();
        }
    }
}
