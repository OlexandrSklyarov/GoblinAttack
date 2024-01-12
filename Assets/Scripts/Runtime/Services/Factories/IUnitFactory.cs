using Game.Runtime.Core.Enemies;
using UnityEngine;

namespace Game.Runtime.Services.Factories
{
    public interface IUnitFactory
    {
        public EnemyUnit CreateUnit(UnitType type, Vector3 position, Quaternion rotation);
    }
}