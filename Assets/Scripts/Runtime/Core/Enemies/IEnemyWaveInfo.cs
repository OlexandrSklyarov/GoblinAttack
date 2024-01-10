using System;

namespace Game.Runtime.Core.Enemies
{
    public interface IEnemyWaveInfo
    {
        event Action<int, int> ChangeWaveProgressEvent;
    }
}