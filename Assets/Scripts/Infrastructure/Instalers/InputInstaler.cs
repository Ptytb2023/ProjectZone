using Services.Input;
using UI;
using UnityEngine;
using Zenject;

namespace Infrastructure.Installers
{
    public class InputInstaler : MonoInstaller
    {
        [SerializeField] private PlayerInputMobile _uIInputModelPrefab;

        public override void InstallBindings()
        {
            var inputModel = Instantiate(_uIInputModelPrefab);

            Container.BindInterfacesAndSelfTo<MobileInputService>().AsSingle().WithArguments(inputModel).NonLazy();
        }
    }
}