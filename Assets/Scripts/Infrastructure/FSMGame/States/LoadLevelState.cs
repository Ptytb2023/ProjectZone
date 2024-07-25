using DataPersistence;
using Services.Input;
using Services.SceneLoaders;

namespace Infrastructure.FSMGame
{
    public class LoadLevelState : IPayloadedState<Scene>
    {
        private readonly IInputService _inputService;
        private readonly IServiceSceneLoader _sceneSercvice;

        private Scene _currenScene;

        public LoadLevelState(IServiceSceneLoader sceneSercvice, IInputService inputService)
        {
            _sceneSercvice = sceneSercvice;
            _inputService = inputService;
        }

        public async void Enter(Scene payload)
        {
            await _sceneSercvice.LoadAsync(payload);
            _inputService.SetActive(true);
        }

        public async void Exit()
        {
            await _sceneSercvice.UnLoadAsync(_currenScene);
            _inputService.SetActive(false);
        }
    }
}
