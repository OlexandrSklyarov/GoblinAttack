using System;

namespace Game.Runtime.Core.Enemies
{
    public interface IEnemyKillInfo
    {
        event Action<int> EnemyKilledEvent;
    }
}