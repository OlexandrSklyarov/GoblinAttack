using Game.Runtime.Util;
using UnityEngine;

namespace Game.Runtime.Core.Components
{
    public class CharacterStats
    {
        public SimpleReactiveProperty<float> SpecialAttackCooldown {get; private set;} = new();     
        
        public void AddSpecialAttackCooldown(float value)
        {
            SpecialAttackCooldown.Value = value;
        }

        public void RestoreSpecialAttackCooldown()
        {
            if (SpecialAttackCooldown.Value > 0f)
            {
                SpecialAttackCooldown.Value -= Time.deltaTime;
            }
        }
    }
}