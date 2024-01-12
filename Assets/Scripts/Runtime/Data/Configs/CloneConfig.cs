using Game.Runtime.Core.Enemies;
using UnityEngine;

namespace Game.Runtime.Data.Configs
{
    [CreateAssetMenu(menuName = "SO/Enemy/CloneConfig", fileName = "CloneConfig")]
    public class CloneConfig : ScriptableObject
    {
        [field: SerializeField] public UnitType Type {get; private set;}
        [field: SerializeField, Min(1)] public int CloneCount {get; private set;} = 2;        
    }
}