using System;
using UnityEngine;

namespace Game.Runtime.Core.Components
{
    public class HealthComponent
    {
        public bool IsAlive => _currentHP > 0;

        public float Value 
        { 
            get => _currentHP;
            set 
            {
                _currentHP = Mathf.Clamp(value, 0f, _maxHP);  
                ChangeValueEvent?.Invoke(_currentHP, _maxHP);
            }          
        }

        private float _maxHP = 100;                    

        private float _currentHP;

        public event Action<float, float> ChangeValueEvent;

        public HealthComponent(int maxHP)
        {
            _maxHP = maxHP;
            Restore();
        }       

        public void Restore() => _currentHP = _maxHP; 

        public void Heal(float value) => _currentHP += value;   

        public void ResizeMaxHP(float value) 
        {
            if (value < 0f)
            {
                Debug.LogWarning($"Value cannot be less than zero!!! value ({value})");
                return;
            }

            _maxHP = value;  
        }             
    }
}