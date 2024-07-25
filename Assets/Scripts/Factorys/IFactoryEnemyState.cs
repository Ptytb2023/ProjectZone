using Enemys;
using Infrastructure.FSMGame;
using UnityEngine;

namespace Factorys
{
    public interface IFactoryEnemyState
    {
        IExitableState CreateAttackState(Transform transform, EnemyData enemyData, EnemyStateMachine stateMachine);
        IExitableState CreateFollowState(Transform transform, EnemyData enemyData, EnemyStateMachine stateMachine);
        IExitableState CreateSearchState(ITriggerObserver triggerObserver, EnemyStateMachine stateMachine);
    }
}