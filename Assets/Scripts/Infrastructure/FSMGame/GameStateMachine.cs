using System;
using System.Collections.Generic;

namespace Infrastructure.FSMGame
{
    public class GameStateMachine
    {
        private Dictionary<Type, IState> _states;

        private IState _activeState = new IState.Empty();

        private void Enter<TState>() where TState : IState
        {
            var typeState = typeof(TState);

            if (_states.TryGetValue(typeState, out var newState) == false)
                throw new ArgumentNullException($"{nameof(typeState)}: This condition has not been registered");

            _activeState?.Exit();
            _activeState = newState;
            _activeState.Enter();
        }

    }
}
