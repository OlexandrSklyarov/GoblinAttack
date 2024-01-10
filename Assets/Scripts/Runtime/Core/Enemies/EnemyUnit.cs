using System;
using Game.Runtime.Core.Components;
using Game.Runtime.Core.Damage;
using Game.Runtime.Data.Configs;
using UnityEngine;

namespace Game.Runtime.Core.Enemies
{
    public class EnemyUnit : MonoBehaviour, IDamageTarget
    {
        bool IDamageTarget.IsAlive => _health.IsAlive;
        Vector3 IDamageTarget.Position => transform.position;

        [SerializeField] private EnemyUnitConfig _config;
        [SerializeField] private HealthComponent _health;

        public event Action<EnemyUnit> DieEvent;

        private void Awake() 
        {
            Init();    
        }

        private void Init() 
        {
            _health.Restore();    
        }

        void IDamageTarget.ApplyDamage(float damage)
        {
            Debug.Log($"damage {damage}");
            _health.Value -= damage;
        }

        public void OnUpdate()
        {            
        }

        public void OnFixedUpdate()
        {
        }
    }
}