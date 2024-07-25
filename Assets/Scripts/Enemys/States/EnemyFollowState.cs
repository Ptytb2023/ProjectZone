using Services;
using System.Collections;
using Infrastructure.FSMGame;
using UnityEngine;
using Player;

namespace Enemys
{
    public class EnemyFollowState : IPayloadedState<Hero>
    {
        private Moveble _moveble;
        private RangeEvaluator _rangeEvaluator;

        private EnemyStateMachine _stateMachine;
        private ICoroutineService _corutineService;

        private Hero _hero;
        private Coroutine _follow;

        public EnemyFollowState(
            Moveble moveble,
            RangeEvaluator rangeEvaluator,
            ICoroutineService corutineService,
            EnemyStateMachine enemyStateMachine)
        {
            _moveble = moveble;
            _rangeEvaluator = rangeEvaluator;
            _stateMachine = enemyStateMachine;
            _corutineService = corutineService;
        }

        public void Enter(Hero hero)
        {
            _hero = hero;
            _hero.Died += OnHeroDied;
            _follow = _corutineService.RunCoroutine(Following(hero.transform));
        }

        public void Exit()
        {
            _hero.Died -= OnHeroDied;
            _corutineService.Stop(_follow);
        }

        private void OnHeroDied() =>
            _stateMachine.Enter<EnemySearchState>();

        private IEnumerator Following(Transform enemy)
        {
            while (true)
            {
                yield return null;

                if (_rangeEvaluator.AttackZoneValidator(enemy.position))
                    _stateMachine.Enter<EnemyAttackState, Hero>(_hero);

                _moveble.Move(enemy);
            }
        }
    }
}
