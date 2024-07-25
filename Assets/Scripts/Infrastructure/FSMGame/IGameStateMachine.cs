namespace Infrastructure.FSMGame
{
    public interface IStateMachine
    {
        void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>;
        void Enter<TState>() where TState : class, IState;

    }

    public interface IGameStateMachine : IStateMachine
    {
    }
}