using DataPersistence;
using Services.Input;
using Services.SceneLoaders;
using System;
using System.Collections.Generic;

namespace Infrastructure.FSMGame
{
    public class GameStateMachine : IGameStateMachine
    {
        private readonly Dictionary<Type, IExitableState> _states;

        private IExitableState _activeState = new IExitableState.Empty();

        public GameStateMachine(IServiceSceneLoader serviceSceneLoader, IInputService inputService)
        {
            _states = new Dictionary<Type, IExitableState>()
            {
                [typeof(LoadLevelState)] = new LoadLevelState(serviceSceneLoader, inputService)
            };
        }

        public void Enter<TState>() where TState : class, IState
        {
            IState state = ChangeState<TState>();
            state.Enter();
        }

        public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>
        {
            TState state = ChangeState<TState>();
            state.Enter(payload);
        }

        private TState ChangeState<TState>() where TState : class, IExitableState
        {
            IExitableState newState = GetState<TState>();

            _activeState?.Exit();
            _activeState = newState;

            return newState as TState;
        }

        private IExitableState GetState<TState>() where TState : class, IExitableState
        {
            var typeState = typeof(TState);

            if (_states.TryGetValue(typeState, out var newState) == false)
                throw new ArgumentNullException($"{nameof(typeState)}: This state has not been registered");

            return newState;
        }
    }
}
