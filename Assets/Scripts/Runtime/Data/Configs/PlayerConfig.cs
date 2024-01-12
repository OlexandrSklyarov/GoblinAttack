using System;
using UnityEngine;

namespace Game.Runtime.Data.Configs
{
    [CreateAssetMenu(menuName = "SO/Payer/PlayerConfig", fileName = "PlayerConfig")]
    public class PlayerConfig : ScriptableObject
    {
        [field: SerializeField, Min(1)] public int MaxHealth {get; private set;} = 100;
        [field: Space, SerializeField] public MovingConfig Moving {get; private set;}
        [field: Space, SerializeField] public RotationConfig Rotation {get; private set;}
        [field: Space, SerializeField] public AttackConfig Attack {get; private set;}
        [field: Space, SerializeField] public SpecialAttackConfig SpecialAttack {get; private set;}
        [field: Space, SerializeField] public TargetScanConfig Scan {get; private set;}
        
        [field: Space, Tooltip("Whether to play the damage animation if a player attacks and takes damage while doing so."),SerializeField] 
        public bool IsPlayDamageAnimation  {get; private set;}
    }

    [Serializable]
    public class RotationConfig
    {
        [field: SerializeField, Min(0.01f)] public float Time{get; private set;} = 0.1f;
        [field: SerializeField, Min(1f)] public float Speed {get; private set;} = 50f;
    }
}