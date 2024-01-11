using System;
using UnityEngine;

namespace Game.Runtime.Data.Configs
{
    [Serializable]
    public class SpecialAttackConfig
    {
        [field: SerializeField, Min(1f)] public float Damage {get; private set;} = 20f;
        [field: SerializeField, Min(1f)] public float AttackCooldown {get; private set;} = 2f;
    }
}