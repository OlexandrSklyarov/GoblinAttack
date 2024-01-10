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
        private void Construct(IPlayerChangeStats playerStats)
        {
            playerStats.ChangedSpecialAttackCooldownEvent += ChangeSpecialAttackProgress;  
            playerStats.ChangedHealthEvent += OnChangeHealth; 
        }       
        
        private void ChangeSpecialAttackProgress(float progress)
        {
            _attackCooldownBar.fillAmount = 1f - progress;
        }

        private void OnChangeHealth(float progress)
        {
            _hpBar.SetProgress(progress);
        }
    }
}