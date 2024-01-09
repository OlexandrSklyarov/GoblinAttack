using UnityEngine;
using System;

namespace Game.Runtime.Core.Animations
{
    public class CharacterAnimationProvider : MonoBehaviour
    {
        public event Action OnAttackExecuteEvent;
        public event Action OnAttackCompleteEvent;
        public event Action OnDamageCompleteEvent;

        private void OnAttackExecute() => OnAttackExecuteEvent?.Invoke();
        private void OnAttackCompleted() => OnAttackCompleteEvent?.Invoke();
        private void OnDamageCompleted() => OnDamageCompleteEvent?.Invoke();
    }
}