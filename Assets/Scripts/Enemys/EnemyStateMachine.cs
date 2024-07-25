using Factorys;
using Infrastructure.FSMGame;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Enemys
{
    public class EnemyStateMachine : StateMachine
    {
        public EnemyStateMachine(EnemyData enemyData, Transform transform, ITriggerObserver triggerObserver, IFactoryEnemyState enemyStateFactory)
        {
            States = new Dictionary<Type, IExitableState>()
            {
                [typeof(EnemySearchState)] = enemyStateFactory.CreateSearchState(triggerObserver, this),
                [typeof(EnemyFollowState)] = enemyStateFactory.CreateFollowState(transform, enemyData, this),
                [typeof(EnemyAttackState)] = enemyStateFactory.CreateAttackState(transform, enemyData, this)
            };
        }

        public void Start() =>
            Enter<EnemySearchState>();

        public void Stop() =>
            ActiveState.Exit();
    }
}
