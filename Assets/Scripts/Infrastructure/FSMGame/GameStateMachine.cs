using Services.Input;
using Services.Save;
using Services.SceneLoaders;
using System;
using System.Collections.Generic;
using Zenject;

namespace Infrastructure.FSMGame
{

    public class GameStateMachine : StateMachine, IGameStateMachine
    {
        public GameStateMachine(IServiceSceneLoader serviceSceneLoader, IInputService inputService,ISaveLoadService saveLoadService, DiContainer diContainer)
        {
            States = new Dictionary<Type, IExitableState>()
            {
                [typeof(LoadLevelState)] = new LoadLevelState(serviceSceneLoader, inputService),
                [typeof(GameStatePreserver)] = new GameStatePreserver(saveLoadService,diContainer)
            };
        }
    }
}
