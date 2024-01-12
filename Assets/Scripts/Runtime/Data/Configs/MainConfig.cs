using UnityEngine;
using Game.Runtime.Core.Player;
using System;
using Game.Runtime.Core.UI;
using Game.Runtime.Util.Data.DataStruct;
using Game.Runtime.Core.Enemies;

namespace Game.Runtime.Data.Configs
{
    [CreateAssetMenu(menuName = "SO/MainConfig",fileName = "MainConfig")]
    public class MainConfig : ScriptableObject
    {
        [field: SerializeField] public PlayerController PlayerPrefab {get; private set;}
        [field: Space, SerializeField] public EnemyManagerConfig EnemyManager {get; private set;}
        [field: Space, SerializeField] public UnitFactoryConfig Factory {get; private set;}
        [field: Space, SerializeField] public UIConfig UI {get; private set;}
    }

    [Serializable]
    public class EnemyManagerConfig
    {
        [field: SerializeField] public EnemyWave[] Waves {get; private set;}
        [field: SerializeField] public RangeFloatValue SpawnRadius{get; private set;} = new RangeFloatValue(5f, 15f);
    }

    [Serializable]
    public class UIConfig
    {
        [field: SerializeField] public PCHud PCHudPrefab {get; private set;}
        [field: SerializeField] public MobileHud MobileHudPrefab {get; private set;}
    }

    [Serializable]
    public class UnitFactoryConfig
    {
        [field: SerializeField] public UnitFactoryItem[] Units {get; private set;}
    }

    [Serializable]
    public class UnitFactoryItem
    {
        [field: SerializeField] public UnitType Type {get; private set;}
        [field: SerializeField] public EnemyUnit UnitPrefab {get; private set;}
    }
}