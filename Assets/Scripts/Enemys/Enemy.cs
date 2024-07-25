﻿using System;
using UnityEngine;

namespace Enemys
{
    [RequireComponent(typeof(Health))]
    public class Enemy : MonoBehaviour, IDamageble
    {
        private Health _health;  
        private EnemyStateMachine _enemyStateMachine;

        public event Action Died;

        private void Awake() => 
            _health = GetComponent<Health>();

        public void Init(EnemyStateMachine enemyStateMachine)=>
            _enemyStateMachine = enemyStateMachine;

        private void OnEnable()
        {
            if (_enemyStateMachine is not null)
                _enemyStateMachine.Start();

            _health.Value.Subscribe(OnChangeHealth);
        }

        private void OnDisable()
        {
            if(_enemyStateMachine is not null)
                _enemyStateMachine.Stop();

            _health.Value.Unsubscribe(OnChangeHealth);
        }

        public void TakeDemage(float damege) =>
           _health.TakeDamage(damege);

        private void OnChangeHealth(float value)
        {
            if (value <= 0)
                Died?.Invoke();

            Destroy(gameObject);
        }
    }
}
