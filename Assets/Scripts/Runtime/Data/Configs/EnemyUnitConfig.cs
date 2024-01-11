using Game.Runtime.Core.Enemies;
using UnityEngine;

namespace Game.Runtime.Data.Configs
{
    [CreateAssetMenu(menuName = "SO/Enemy/EnemyUnitConfig", fileName = "EnemyUnitConfig")]
    public class EnemyUnitConfig : ScriptableObject
    {
        [field: SerializeField] public MovingConfig Moving {get; private set;}
        [field: SerializeField] public RotationConfig Rotation {get; private set;}
        [field: SerializeField] public AttackConfig Attack {get; private set;}
    }
}