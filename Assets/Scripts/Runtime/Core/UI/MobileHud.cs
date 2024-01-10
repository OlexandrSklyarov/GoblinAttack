using Game.Runtime.Core.Enemies;
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
        private void Construct(IPlayerChangeStats playerStats, IEnemyWaveInfo waveInfo)
        {
            playerStats.RestoreSpecialAttackEvent += ChangeSpecialAttackProgress;  
            playerStats.ChangeSpecialAttackStatusEvent += ChangeSpecialAttackStatus;  
            playerStats.ChangedHealthEvent += OnChangeHealth; 

            waveInfo.ChangeWaveProgressEvent += OnChangeWaveProgress;
        }

        private void ChangeSpecialAttackStatus(bool isActive)
        {
            _specialAttackButton.SetActiveInteraction(isActive);
        }

        private void ChangeSpecialAttackProgress(float progress)
        {
            _specialAttackButton.SetActiveProgress(1f - progress);
        }        
    }
}