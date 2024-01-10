using System;

namespace Game.Runtime.Core.Components
{
    public interface ICharacterStats
    {
        event Action<float> ChangedSpecialAttackCooldownEvent;
    }
}