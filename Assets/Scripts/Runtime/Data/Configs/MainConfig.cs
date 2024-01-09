using UnityEngine;
using Game.Runtime.Core.Player;
using System;

namespace Game.Runtime.Data.Configs
{
    [CreateAssetMenu(menuName = "SO/MainConfig",fileName = "MainConfig")]
    public class MainConfig : ScriptableObject
    {
        [field: SerializeField] public PlayerController PlayerPrefab {get; private set;}
        [field: Space, SerializeField] public EnemyManagerConfig EnemyManager {get; private set;}
    }

    [Serializable]
    public class EnemyManagerConfig
    {
        [field: SerializeField] public EnemyWave[] Waves {get; private set;}
    }
}