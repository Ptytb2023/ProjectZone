using Loots;
using Services;

using UnityEngine;
using Zenject;
namespace Enemys
{
    [RequireComponent(typeof(Enemy))]
    public class EnemySpawnerLoot : MonoBehaviour
    {
        [SerializeField] private int _minSpawn;
        [SerializeField] private int _maxSpawn;

        private Enemy _enemy;

        private IItemService _itemService;
        private IItemDropper _itemDroper;

        private void Awake() => 
            _enemy = GetComponent<Enemy>();

        [Inject]
        public void Construct(IItemService itemService, IItemDropper itemDropper)
        {
            _itemDroper = itemDropper;
            _itemService = itemService;
        }

        private void OnEnable() =>
            _enemy.Died += OnEnemyDied;



        private void OnDisable() =>
            _enemy.Died -= OnEnemyDied;


        private void OnEnemyDied(Enemy enemy)
        {
            int countSpawn = Random.Range(_minSpawn, _maxSpawn);

            for (int i = 0; i < countSpawn; i++)
            {
                var item = _itemService.GetRandomItem();

                _itemDroper.DropItem(transform, item.Id);
            }
        }

    }
}
