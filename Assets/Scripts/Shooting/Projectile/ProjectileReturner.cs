using PoolObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using Zenject;

using Random = UnityEngine.Random;

namespace Shooting.Projectiles
{
    [RequireComponent(typeof(Projectile))]
    public class ProjectileReturner : MonoBehaviour
    {
        [SerializeField] private float _minTime;
        [SerializeField] private float _maxTime;

        private Projectile _projectile;
        private IPool<Projectile> _pool;
        
        private float _time;


        [Inject]
        private void Construct(IPool<Projectile> pool)=>
            _pool = pool;

        private void Awake() => 
            _projectile = GetComponent<Projectile>();

        private void OnEnable() => 
            _time = Random.Range(_minTime, _maxTime);


        private void Update()
        {
            _time-=Time.deltaTime;

            if (_time <= 0)
                _pool.Return(_projectile);
        }
    }
}
