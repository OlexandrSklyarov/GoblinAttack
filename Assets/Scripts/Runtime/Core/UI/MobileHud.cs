using Game.Runtime.Core.Player;
using Game.Runtime.UI;
using TMPro;
using UnityEngine;
using VContainer;

namespace Game.Runtime.Core.UI
{
    public class MobileHud : BaseHud 
    {
        [SerializeField] private ActionButton _specialAttackButton; 

        [Inject]
        private void Construct(PlayerController player)
        {
            player.Stats.ChangedSpecialAttackCooldownEvent += ChangeSpecialAttackProgress;  
            player.Health.ChangeValueEvent += OnChangeHealth; 
        } 

        private void OnChangeHealth(float currentValue, float maxValue)
        {
            _hpBar.SetProgress(currentValue / maxValue);
        }

        private void ChangeSpecialAttackProgress(float progress)
        {
            _specialAttackButton.SetActiveProgress(1f - progress);
        }
    }
}