using System;
using System.Collections.Generic;

namespace Infrastructure.FSMGame
{
    public abstract class StateMachine : IStateMachine 
    {
        protected Dictionary<Type, IExitableState> States;

        protected IExitableState ActiveState = new IExitableState.Empty();

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

            ActiveState?.Exit();
            ActiveState = newState;

            return newState as TState;
        }

        private IExitableState GetState<TState>() where TState : class, IExitableState
        {
            var typeState = typeof(TState);

            if (States.TryGetValue(typeState, out var newState) == false)
                throw new ArgumentNullException($"{nameof(typeState)}: This state has not been registered");

            return newState;
        }
    }
}
