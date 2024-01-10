using System;
using Game.Runtime.Core.Enemies;
using Game.Runtime.Core.Player;
using UnityEngine;
using VContainer;

namespace Game.Runtime.Core.UI
{
    public class PCHud : BaseHud 
    {        
        [SerializeField] protected StatusIconView _attackCooldownBar;
       
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
            _attackCooldownBar.SetActiveInteraction(isActive);
        }
        
        private void ChangeSpecialAttackProgress(float progress)
        {
            _attackCooldownBar.SetActiveProgress(1f - progress);
        }

        
    }
}