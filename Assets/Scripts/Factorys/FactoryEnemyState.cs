using Enemys;
using Infrastructure.FSMGame;
using Services;
using UnityEngine;

namespace Factorys
{
    public class FactoryEnemyState : IFactoryEnemyState
    {
        private readonly ICoroutineService _corutineService;
        private readonly EnemyStateMachine _stateMachine;

        public FactoryEnemyState(ICoroutineService corutineService) =>
            _corutineService = corutineService;

        public IExitableState CreateSearchState(ITriggerObserver triggerObserver, EnemyStateMachine stateMachine) =>
            new EnemySearchState(triggerObserver, stateMachine);

        public IExitableState CreateFollowState(Transform transform, EnemyData enemyData, EnemyStateMachine stateMachine)
        {
            Moveble moveble = new Moveble(transform, enemyData.MoveSpeed);
            RangeEvaluator rangeEvaluator = CreatRange(transform, enemyData);

            return new EnemyFollowState(moveble, rangeEvaluator, _corutineService, stateMachine);
        }

        public IExitableState CreateAttackState(Transform transform, EnemyData enemyData, EnemyStateMachine stateMachine)
        {
            RangeEvaluator rangeEvaluator = CreatRange(transform, enemyData);

            return new EnemyAttackState(enemyData, _corutineService, stateMachine, rangeEvaluator);
        }

        private RangeEvaluator CreatRange(Transform transform, EnemyData enemyData) =>
           new RangeEvaluator(enemyData.DistanceAttack, transform);
    }

}
