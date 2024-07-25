using Enemys;

namespace Factorys
{
    public interface IFactoryEnemy
    {
        Enemy Create(Enemy enemyPrefab, EnemyData enemyData);
    }
}