using Services.Input;
using Services.SceneLoaders;
using System;
using System.Collections.Generic;

namespace Infrastructure.FSMGame
{

    public class GameStateMachine : StateMachine, IGameStateMachine
    {
        public GameStateMachine(IServiceSceneLoader serviceSceneLoader, IInputService inputService)
        {
            States = new Dictionary<Type, IExitableState>()
            {
                [typeof(LoadLevelState)] = new LoadLevelState(serviceSceneLoader, inputService)
            };
        }
    }
}
