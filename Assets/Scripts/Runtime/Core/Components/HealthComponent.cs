using System;
using UnityEngine;

namespace Game.Runtime.Core.Components
{
    public class HealthComponent : MonoBehaviour
    {
        public bool IsAlive => _currentHP > 0;

        public float Value 
        { 
            get => _currentHP;
            set 
            {
                _currentHP = Mathf.Clamp(value, 0f, _maxHP);  
                ChangeValueEvent?.Invoke(_currentHP);
            }          
        }

        [SerializeField] private float _maxHP = 100;                    

        private float _currentHP;

        public event Action<float> ChangeValueEvent;

        private void Awake() 
        {
            Restore();
        }

        public void Restore() => _currentHP = _maxHP; 

        public void Heal(float value) => _currentHP += value;   

        public void ResizeMaxHP(float value) 
        {
            if (value < 0f)
            {
                Debug.LogWarning($"max hp value  {value} < 0!!!");
                return;
            }

            _maxHP = value;  
        }             
    }
}