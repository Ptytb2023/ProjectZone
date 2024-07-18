namespace Infrastructure.FSMGame
{
    public interface IGameStateMachine
    {
        public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>;
        public void Enter<TState>() where TState : class, IState;
    }
}