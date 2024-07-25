using Infrastructure.FSMGame;
using UnityEngine;
using Zenject;

namespace Infrastructure
{
    public class SaveTrigger : MonoBehaviour
    {
        private IGameStateMachine _gameStateMachine;

        [Inject]
        public void Construct(IGameStateMachine gameStateMachine)=>
            _gameStateMachine = gameStateMachine;

        private void Start() => 
            DontDestroyOnLoad(gameObject);

        private void OnApplicationQuit() => 
            _gameStateMachine.Enter<GameStatePreserver>();
    }
}
