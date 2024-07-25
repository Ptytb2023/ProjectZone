using DataPersistence;
using Infrastructure.FSMGame;
using Infrastructure.Initialization;
using Services.Save;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Infrastructure
{
    public class GameBootstrap : MonoBehaviour
    {
        [SerializeField] private AsyncInitialization[] _initializations;
        [SerializeField] private Scene _levelScene;

        private IPermissionService _permissionService;
        private IGameStateMachine _gameStateMachine;

        [Inject]
        public void Construct(IGameStateMachine serviceSceneLoader, IPermissionService permision)
        {
            _gameStateMachine = serviceSceneLoader;
        }

        private async void Start()
        {
            if (Application.platform == RuntimePlatform.Android)
                _permissionService.PermissionReques();

            var instializtions = _initializations.Select(x => x.InitializeAsync());

            await Task.WhenAll(instializtions);

            _gameStateMachine.Enter<LoadLevelState, Scene>(_levelScene);
        }
    }
}
