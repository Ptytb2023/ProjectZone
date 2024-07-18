using Services.Input;
using UI;
using UnityEngine;
using Zenject;

namespace Infrastructure.Installers
{
    public class InputInstaler : MonoInstaller
    {
        [SerializeField] private UIInputModel _uIInputModulPrefab;

        public override void InstallBindings()
        {
            var inputModul = Container.InstantiatePrefab(_uIInputModulPrefab);
            Container.Bind<IInputService>().To<MobileInputService>().WithArguments(inputModul);
        }
    }
}