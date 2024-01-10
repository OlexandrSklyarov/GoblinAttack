using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Game.Runtime.Core.Damage;
using Game.Runtime.Data.Configs;

namespace Game.Runtime.Core.Enemies
{
    public class EnemySpawnController : IEnemyWaveInfo
    {
        private readonly MainConfig _config;
        private readonly IPlayerDamageTarget _player;
        private readonly List<EnemyUnit> _units = new();

        public event Action AllUnitsKillEvent;
        public event Action<int, int> ChangeWaveProgressEvent;

        public EnemySpawnController(MainConfig config, IPlayerDamageTarget player)
        {
            _config = config;
            _player = player;
        }


        public async void StartSpawn()
        {
            UnityEngine.Debug.Log("start spawn enemies");
            await UniTask.WaitForSeconds(1f);
            ChangeWaveProgressEvent?.Invoke(5, 10);

            await UniTask.WaitForSeconds(1f);
            ChangeWaveProgressEvent?.Invoke(5, 10);

            await UniTask.WaitForSeconds(1f);
            ChangeWaveProgressEvent?.Invoke(5, 10);

            await UniTask.WaitForSeconds(1f);
            ChangeWaveProgressEvent?.Invoke(5, 10);

        }

        public void StopSpawn()
        {
            UnityEngine.Debug.Log("start spawn enemies");
            
        }
    }
}