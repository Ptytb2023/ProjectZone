namespace Infrastructure.FSMGame
{
    public interface IState : IExitableState
    {
        public void Enter();
    }

    public interface IPayloadedState<TPayload> : IExitableState
    {
        public void Enter(TPayload payload);
    }

    public interface IExitableState
    {
        public void Exit();

        public class Empty : IExitableState
        {
            public void Exit() { }
        }
    }
}
