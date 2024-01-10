using System;
using UnityEngine;

namespace Game.Runtime.Core.Components
{
    public class CharacterStats : ICharacterStats
    {

        public float SpecialAttackCooldown 
        {
            get => _specialAttackCooldown;
            private set 
            {
                _specialAttackCooldown = value;
                ChangedSpecialAttackCooldownEvent?.Invoke(_specialAttackCooldown / _maxSpecialAttackCooldown);
            }
        }   

        private float _maxSpecialAttackCooldown;
        private float _specialAttackCooldown;

        public event Action<float> ChangedSpecialAttackCooldownEvent;
        
        public void AddSpecialAttackCooldown(float value)
        {
            _maxSpecialAttackCooldown = value;
            SpecialAttackCooldown = value;
        }

        public void RestoreSpecialAttackCooldown()
        {
            if (SpecialAttackCooldown > 0f)
            {
                SpecialAttackCooldown -= Time.deltaTime;
            }
        }
    }
}