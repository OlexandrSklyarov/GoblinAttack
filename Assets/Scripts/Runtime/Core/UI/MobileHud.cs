using Game.Runtime.Core.Player;
using Game.Runtime.UI;
using UnityEngine;
using VContainer;

namespace Game.Runtime.Core.UI
{
    public class MobileHud : BaseHud 
    {
        [SerializeField] private ActionButton _specialAttackButton; 

        [Inject]
        private void Construct(IPlayerChangeStats playerStats)
        {
            playerStats.ChangedSpecialAttackCooldownEvent += ChangeSpecialAttackProgress;  
            playerStats.ChangedHealthEvent += OnChangeHealth; 
        } 

        private void OnChangeHealth(float progress)
        {
            _hpBar.SetProgress(progress);
        }

        private void ChangeSpecialAttackProgress(float progress)
        {
            _specialAttackButton.SetActiveProgress(1f - progress);
        }        
    }
}