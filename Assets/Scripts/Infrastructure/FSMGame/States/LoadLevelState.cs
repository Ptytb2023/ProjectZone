using Data;
using Services.SceneLoaders;
using System;

namespace Infrastructure.FSMGame
{

    public class LoadLevelState : IPayloadedState<Scene>
    {
        private readonly IServiceSceneLoader _serviceProvider;

        public LoadLevelState(IServiceSceneLoader serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void Enter(Scene payload)
        {
            _serviceProvider.LoadAsync(payload);
        }

        public void Exit()
        {
            throw new NotImplementedException();
        }
    }
}
