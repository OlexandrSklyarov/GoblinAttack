using System;
using UnityEngine;

namespace Game.Runtime.Data.Configs
{
    [Serializable]
    public class TargetScanConfig
    {
        [field: SerializeField, Min(0.01f)] public float ScanDelay {get; private set;} = 0.5f;
    }
}