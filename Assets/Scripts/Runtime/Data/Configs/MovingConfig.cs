using System;
using UnityEngine;

namespace Game.Runtime.Data.Configs
{
    [Serializable]
    public class MovingConfig
    {
        [field: SerializeField, Min(1f)] public float Acceleration {get; private set;} = 50f;
        [field: SerializeField, Min(1f)] public float MaxSpeed {get; private set;} = 5f;
    }
}