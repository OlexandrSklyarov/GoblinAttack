using Game.Runtime.Core.Player;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace Game.Runtime.Core.UI
{
    public class Hud : BaseHud 
    {        
        [SerializeField] protected Image _attackCooldownBar;
       
        [Inject]
        private void Construct(PlayerController player)
        {
            player.Stats.ChangedSpecialAttackCooldownEvent += ChangeSpecialAttackProgress;  
            player.Health.ChangeValueEvent += OnChangeHealth; 
        }       
        
        private void ChangeSpecialAttackProgress(float progress)
        {
            _attackCooldownBar.fillAmount = 1f - progress;
        }

        private void OnChangeHealth(float currentValue, float maxValue)
        {
            _hpBar.SetProgress(currentValue / maxValue);
        }
    }
}