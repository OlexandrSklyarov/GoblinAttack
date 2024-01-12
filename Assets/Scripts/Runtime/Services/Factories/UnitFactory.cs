using System;
using System.Collections.Generic;
using System.Linq;
using Game.Runtime.Core.Enemies;
using Game.Runtime.Core.Services.ObjectPool;
using Game.Runtime.Data.Configs;
using Game.Runtime.Services.Factories;
using UnityEngine;

namespace SA.Runtime.Core.Services.Factories
{
    public sealed class UnitFactory : IUnitFactory, IDisposable
    {
        private MainConfig _config;
        private Transform _container;
        private Dictionary<UnitType, UniversalPoolGO<EnemyUnit>> _enemyUnitPools = new();

        public UnitFactory(MainConfig config)
        {
            _config = config;
            _container = new GameObject("[Units_Container]").transform;   
        }                    
        
        EnemyUnit IUnitFactory.CreateUnit(UnitType type, Vector3 position, Quaternion rotation)
        {
            var pool = GetPool(type);
            var unit = pool.Get();
            unit.transform.SetPositionAndRotation(position, rotation);
            return unit;
        }

        private UniversalPoolGO<EnemyUnit> GetPool(UnitType type)
        {
            if (!_enemyUnitPools.TryGetValue(type, out var pool))
            {
                pool = new UniversalPoolGO<EnemyUnit>
                (
                    _config.Factory.Units.First(x => x.Type == type).UnitPrefab,
                    $"[{type}]_POOL"
                );
                
                _enemyUnitPools.Add(type, pool);
            }

            return pool;
        }

        public void Dispose()
        {
            foreach(var pool in _enemyUnitPools)
            {
                pool.Value.Clear();
            }
        }
    }
}