using System;
using Game.Runtime.Core.Enemies;
using NaughtyAttributes;
using UnityEngine;

namespace Game.Runtime.Data.Configs
{
    [CreateAssetMenu(menuName = "SO/Enemy/EnemyUnitConfig", fileName = "EnemyUnitConfig")]
    public class EnemyUnitConfig : ScriptableObject
    {
        [field: SerializeField, Min(1)] public int MaxHealth {get; private set;} = 20;
        [field: Space, SerializeField] public MovingConfig Moving {get; private set;}
        [field: Space, SerializeField] public RotationConfig Rotation {get; private set;}
        [field: Space, SerializeField] public AttackConfig Attack {get; private set;}
        [field: Space, SerializeField] public CloneConfig Clone {get; private set;}
        [field: Space, SerializeField, Min(1)] public int KillRewardPoints {get; private set;} = 2;
    }

    [Serializable]
    public class CloneConfig
    {
        public bool IsCreateClonesOnDeath => _isCreateClonesOnDeath;
        public EnemyUnit UnitPrefab => _unitPrefab;
        public int CloneCount => _cloneCount;
        
        [SerializeField] private bool _isCreateClonesOnDeath;
        [ShowIf("_isCreateClonesOnDeath"), SerializeField] private EnemyUnit _unitPrefab;
        [ShowIf("_isCreateClonesOnDeath"), SerializeField] private int _cloneCount;
    }
}