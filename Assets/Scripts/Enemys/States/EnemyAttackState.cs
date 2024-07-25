using Services;
using System.Collections;
using Infrastructure.FSMGame;
using UnityEngine;
using Player;

namespace Enemys
{
    public class EnemyAttackState : IPayloadedState<Hero>
    {
        private readonly EnemyStateMachine _enemyStateMachine;

        private readonly RangeEvaluator _rangeEvaluator;
        private readonly EnemyData _enemyData;

        private ICoroutineService _corutineService;

        private Hero _hero;

        private WaitForSeconds _attacRate;
        private Coroutine _prossesAttack;


        public EnemyAttackState(
            EnemyData dataAttack,
            ICoroutineService corutineService,
            EnemyStateMachine enemyStateMachine,
            RangeEvaluator rangeEvaluator)
        {
            _enemyStateMachine = enemyStateMachine;
            _enemyData = dataAttack;
            _corutineService = corutineService;

            float delay = dataAttack.DelayBetweenAttack;
            _attacRate = new WaitForSeconds(delay);
            _rangeEvaluator = rangeEvaluator;
        }

        public void Enter(Hero hero)
        {
            _hero = hero;
            _prossesAttack = _corutineService.RunCoroutine(ProcessAttack());
            _hero.Died += OnHeroDead;
        }

        public void Exit()
        {
            _hero.Died -= OnHeroDead;
            _corutineService.Stop(_prossesAttack);
        }

        private void OnHeroDead() =>
            _enemyStateMachine.Enter<EnemySearchState>();

        private IEnumerator ProcessAttack()
        {
            float distanceAttack = _enemyData.DistanceAttack;

            while (true)
            {
                _hero.TakeDemage(_enemyData.Damage);
                yield return _attacRate;

                Vector3 position = _hero.transform.position;

                if (!_rangeEvaluator.AttackZoneValidator(position))
                    _enemyStateMachine.Enter<EnemyFollowState, Hero>(_hero);
            }
        }
    }
}
