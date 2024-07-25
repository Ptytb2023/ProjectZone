using Enemys;
using UnityEngine;

namespace Factorys
{
    public class FactoryEnemy : IFactoryEnemy
    {
        private IFactoryEnemyState _enemyStateFactory;
        private IFactoryObject _factoryObject;

        public FactoryEnemy(IFactoryEnemyState enemyStateFactory, IFactoryObject factoryObject)
        {
            _enemyStateFactory = enemyStateFactory;
            _factoryObject = factoryObject;
        }

        public Enemy Creat(Enemy enemyPrefab, EnemyData enemyData)
        {
            Enemy enemy = _factoryObject.Creat(enemyPrefab);

            TriggerObserver triggerObserver = CreatTrigger(enemy, enemyData);

            Transform transform = enemy.transform;

            EnemyStateMachine enemyStateMachine =
                new EnemyStateMachine(enemyData, transform, triggerObserver, _enemyStateFactory);

            enemy.Init(enemyStateMachine);

            enemy.gameObject.SetActive(false);

            return enemy;
        }

        private static TriggerObserver CreatTrigger(Enemy enemy, EnemyData enemyData)
        {
            CircleCollider2D collider = enemy.gameObject.AddComponent<CircleCollider2D>();
            collider.isTrigger = true;

            TriggerObserver triggerObserver = enemy.gameObject.AddComponent<TriggerObserver>();
            triggerObserver.SetRadius(enemyData.RadiusAgro);
            return triggerObserver;
        }
    }
}
