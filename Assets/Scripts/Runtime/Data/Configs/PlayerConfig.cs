using System;
using UnityEngine;

namespace Game.Runtime.Data.Configs
{
    [CreateAssetMenu(menuName = "SO/Payer/PlayerConfig", fileName = "PlayerConfig")]
    public class PlayerConfig : ScriptableObject
    {
        [field: SerializeField] public MovingConfig Moving {get; private set;}
        [field: Space, SerializeField] public RotationConfig Rotation {get; private set;}
        [field: Space, SerializeField] public AttackConfig Attack {get; private set;}
    }

    [Serializable]
    public class MovingConfig
    {
        [field: SerializeField, Min(1f)] public float Acceleration {get; private set;} = 50f;
        [field: SerializeField, Min(1f)] public float MaxSpeed {get; private set;} = 5f;
    }

    [Serializable]
    public class RotationConfig
    {
        [field: SerializeField, Min(0.01f)] public float Time{get; private set;} = 0.1f;
        [field: SerializeField, Min(1f)] public float Speed {get; private set;} = 50f;
    }

    [Serializable]
    public class AttackConfig
    {
        [field: SerializeField, Min(1f)] public float Damage {get; private set;} = 10f;
        [field: SerializeField, Min(1f)] public float SpecialDamage {get; private set;} = 20f;
        [field: SerializeField, Min(1f)] public float SpecialAttackCooldown {get; private set;} = 2f;
        [field: SerializeField, Min(1f)] public float Range {get; private set;} = 2f;
        [field: SerializeField, Min(0.01f)] public float ScanTargetDelay {get; private set;} = 0.5f;
        [field: SerializeField] public LayerMask TargetLayer {get; private set;}
    }
}