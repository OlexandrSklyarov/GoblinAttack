using Game.Runtime.Core.Enemies;
using UnityEngine;

namespace Game.Runtime.Data.Configs
{
    [CreateAssetMenu(menuName = "SO/Enemy/EnemyWave",fileName = "EnemyWave")]
    public class EnemyWave : ScriptableObject
    {
        [field: SerializeField] public EnemyUnit[] EnemyPrefabs {get; private set;}
        [field: SerializeField] public AnimationCurve CountCurve {get; private set;}
        [field: SerializeField, Min(1f)] public float Duration {get; private set;} = 2f;
        [field: SerializeField, Min(1)] public int MaxCount {get; private set;} = 20;
    }
}