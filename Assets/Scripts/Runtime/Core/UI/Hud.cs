using Game.Runtime.Core.Player;
using UnityEngine;
using VContainer;

namespace Game.Runtime.Core.UI
{
    public class Hud : BaseHud 
    {        
        [SerializeField] protected StatusIconView _attackCooldownBar;
       
        [Inject]
        private void Construct(IPlayerChangeStats playerStats)
        {
            playerStats.RestoreSpecialAttackEvent += ChangeSpecialAttackProgress;  
            playerStats.ChangeSpecialAttackStatusEvent += ChangeSpecialAttackStatus;  
            playerStats.ChangedHealthEvent += OnChangeHealth; 
        }       

        private void ChangeSpecialAttackStatus(bool isActive)
        {
            _attackCooldownBar.SetActiveInteraction(isActive);
        }
        
        private void ChangeSpecialAttackProgress(float progress)
        {
            _attackCooldownBar.SetActiveProgress(1f - progress);
        }

        private void OnChangeHealth(float progress)
        {
            _hpBar.SetProgress(progress);
        }
    }
}