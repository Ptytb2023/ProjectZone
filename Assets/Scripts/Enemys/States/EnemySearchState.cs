using Player;
using Infrastructure.FSMGame;
using UnityEngine;

namespace Enemys
{
    public class EnemySearchState : IState
    {
        private EnemyStateMachine _enemyStateMachine;
        private ITriggerObserver _zoneTrigger;

        private Hero _hero;

        public EnemySearchState(ITriggerObserver zoneTrigger, EnemyStateMachine enemyStateMachine)
        {
            _enemyStateMachine = enemyStateMachine;
            _zoneTrigger = zoneTrigger;
        }

        public void Enter() =>
            _zoneTrigger.TrigerEnter += OnTrigerEnter;

        public void Exit() =>
            _zoneTrigger.TrigerEnter -= OnTrigerEnter;

        private void OnTrigerEnter(Collider2D collider)
        {
            if (!collider.TryGetComponent(out Hero hero))
                return;

            _enemyStateMachine.Enter<EnemyFollowState, Hero>(hero);
        }
    }
}
