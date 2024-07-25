using Enemys;

namespace Factorys
{
    public interface IFactoryEnemy
    {
        Enemy Creat(Enemy enemyPrefab, EnemyData enemyData);
    }
}