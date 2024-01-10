using System;
using System.Collections.Generic;
using System.Linq;
using Game.Runtime.Core.Damage;
using Game.Runtime.Data.Configs;
using Game.Runtime.Util.Extensions;
using UnityEngine;

namespace Game.Runtime.Core.Components
{
    public class TargetSensor
    {
        public IEnumerable<IDamageTarget> Targets => _targets;
        public bool IsTargetExist => _targets.Count > 0;

        private readonly Transform _owner;
        private readonly AttackConfig _attackConfig;
        private readonly Collider[] _results = new Collider[10]; 
        private List<IDamageTarget> _targets = new();
        private float _nextScanTime;

        public event Action<bool> ScanTargetResultEvent;

        public TargetSensor(Transform owner, AttackConfig attackConfig)
        {           
            _owner = owner;
            _attackConfig = attackConfig;
        }
        
        public void ScanTargets()
        {
            if (Time.time < _nextScanTime) return;

            _nextScanTime = Time.time + _attackConfig.ScanTargetDelay;

            _targets.Clear();

            var count = Physics.OverlapSphereNonAlloc
            (
                _owner.position,
                _attackConfig.Range,
                _results,
                _attackConfig.TargetLayer
            );

            if (count > 0)
            {
                _targets = _results.Where(x => x != null)
                    .Select(c => c.GetComponent<IDamageTarget>())
                    .Where(t => t != null)
                    .OrderBy(t => _owner.position.GetDistanceXZ(t.Position))
                    .ToList();
            }
            
            ScanTargetResultEvent?.Invoke(IsTargetExist);
        }
    }
}