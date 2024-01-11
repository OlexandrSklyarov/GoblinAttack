using UnityEngine;

namespace Game.Runtime.Data.Configs
{
    [CreateAssetMenu(menuName = "SO/Enemy/EnemyUnitConfig", fileName = "EnemyUnitConfig")]
    public class EnemyUnitConfig : ScriptableObject
    {
        [field: SerializeField] public MovingConfig Moving {get; private set;}
        [field: Space, SerializeField] public RotationConfig Rotation {get; private set;}
        [field: Space, SerializeField] public AttackConfig Attack {get; private set;}
        [field: Space, SerializeField, Min(1)] public int KillRewardPoints {get; private set;} = 2;
    }
}