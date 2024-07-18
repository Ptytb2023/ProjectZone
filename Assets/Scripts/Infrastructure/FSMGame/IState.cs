namespace Infrastructure.FSMGame
{
    public interface IState
    {
        public void Enter();
        public void Exit();

        public class Empty : IState
        {
            public void Enter() { }
            public void Exit() { }
        }
    }
}
