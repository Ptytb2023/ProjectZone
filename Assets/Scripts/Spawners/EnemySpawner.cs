using System;
using Enemys;
using Factorys;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

using Random = UnityEngine.Random;

namespace Spawners
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private List<MarkerSpawn> _markers;
        [SerializeField] private int _countSpawn;

        [Space]
        [SerializeField] private Enemy _enemyPrefab;
        [SerializeField] private EnemyData _enemyData;

        private IFactoryEnemy _factory;

        private List<Vector3> _occupiedPoints;

        [Inject]
        public void Consctruct(IFactoryEnemy factoryEnemy) =>
            _factory = factoryEnemy;

        private void Start()
        {
            _occupiedPoints = new List<Vector3>();
            Spawn();
        }

        private void Spawn()
        {
            for (int i = 0; i < _countSpawn; i++)
            {
                Enemy enemy = _factory.Create(_enemyPrefab, _enemyData);

                Vector3 spawnPostion = GetRandomPostion();

                enemy.transform.position = spawnPostion;

                enemy.gameObject.SetActive(true);
            }
        }

        private Vector3 GetRandomPostion()
        {
            bool isOccupied;
            MarkerSpawn marker;

            do
            {
                int index = Random.Range(0, _markers.Count);

                marker = _markers[index];

                isOccupied = _occupiedPoints.Any(point => point == marker.Position);

            } while (isOccupied);

            _occupiedPoints.Add(marker.Position);

            return marker.Position;
        }

        private void OnValidate()
        {
            if (_markers is null)
                return;

            if (_markers.Count < _countSpawn)
                throw new ArgumentException($"less than the required amount for spawning ({_countSpawn})." +
                    $" Please ensure that you have added a sufficient number of markers before starting the spawn");
        }
    }
}
