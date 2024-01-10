using System;

namespace Game.Runtime.Core.Player
{
    public interface IPlayerChangeStats
    {
        event Action<float> ChangedSpecialAttackCooldownEvent;
        event Action<float> ChangedHealthEvent;
    }
}