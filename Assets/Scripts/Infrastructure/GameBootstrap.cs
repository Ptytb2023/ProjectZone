using DataPersistence;
using Infrastructure.FSMGame;
using UnityEngine;
using Zenject;

namespace Infrastructure
{
    public class GameBootstrap : MonoBehaviour
    {
        [SerializeField] private Scene _levelScene; 

        private IGameStateMachine _gameStateMachine;

        [Inject]
        public void Construct(IGameStateMachine serviceSceneLoader)
        {
            _gameStateMachine = serviceSceneLoader;
        }

        private void Start()
        {
            _gameStateMachine.Enter<LoadLevelState, Scene>(_levelScene);
        }
    }
}
