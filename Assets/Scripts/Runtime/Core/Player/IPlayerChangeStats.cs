using System;

namespace Game.Runtime.Core.Player
{
    public interface IPlayerChangeStats
    {
        event Action<float> RestoreSpecialAttackEvent;
        event Action<float> ChangedHealthEvent;
        event Action<bool> ChangeSpecialAttackStatusEvent;
    }
}