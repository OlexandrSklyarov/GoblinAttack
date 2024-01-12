using Game.Runtime.Core.Enemies;
using UnityEngine;

namespace Game.Runtime.Data.Configs
{
    [CreateAssetMenu(menuName = "SO/Enemy/EnemyWave",fileName = "EnemyWave")]
    public class EnemyWave : ScriptableObject
    {
        [field: SerializeField] public UnitType[] EnemyTypes {get; private set;}
        [field: SerializeField, Min(1)] public int MaxCount {get; private set;} = 20;
    }
}