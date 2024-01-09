using System;
using UnityEngine;

namespace Game.Runtime.Data.Configs
{
    [CreateAssetMenu(menuName = "SO/Payer/PlayerConfig", fileName = "PlayerConfig")]
    public class PlayerConfig : ScriptableObject
    {
        [field: SerializeField] public MovingConfig Moving {get; private set;}
        [field: SerializeField] public RotationConfig Rotation {get; private set;}
        [field: SerializeField] public AttackConfig Attack {get; private set;}
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
        [field: SerializeField, Min(1f)] public float Duration {get; private set;} = 2f;
        [field: SerializeField, Min(1f)] public float SuperAttackCooldown {get; private set;} = 2f;

    }
}