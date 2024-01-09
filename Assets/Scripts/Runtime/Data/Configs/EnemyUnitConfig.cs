using Game.Runtime.Core.Enemies;
using UnityEngine;

namespace Game.Runtime.Data.Configs
{
    [CreateAssetMenu(menuName = "SO/Enemy/EnemyUnitConfig", fileName = "EnemyUnitConfig")]
    public class EnemyUnitConfig : ScriptableObject
    {
        [field: SerializeField, Min(1f)] public float Speed {get; private set;} = 5f;
        [field: SerializeField, Min(1f)] public float AttackDuration {get; private set;} = 2f;
    }
}