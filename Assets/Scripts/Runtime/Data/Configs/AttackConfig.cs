using System;
using UnityEngine;

namespace Game.Runtime.Data.Configs
{
    [Serializable]
    public class AttackConfig
    {
        [field: SerializeField, Min(1f)] public float Damage {get; private set;} = 10f;
        [field: SerializeField, Min(1f)] public float Range {get; private set;} = 2f;
        [field: SerializeField] public LayerMask TargetLayer {get; private set;}
    }
}