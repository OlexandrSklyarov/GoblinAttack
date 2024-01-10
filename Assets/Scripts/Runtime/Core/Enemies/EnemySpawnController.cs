using System;
using System.Collections.Generic;
using Game.Runtime.Core.Damage;
using Game.Runtime.Data.Configs;

namespace Game.Runtime.Core.Enemies
{
    public class EnemySpawnController
    {
        private readonly MainConfig _config;
        private readonly IPlayerDamageTarget _player;
        private readonly List<EnemyUnit> _units = new();

        public event Action AllUnitsKillEvent;

        public EnemySpawnController(MainConfig config, IPlayerDamageTarget player)
        {
            _config = config;
            _player = player;
        }


        public void StartSpawn()
        {
            UnityEngine.Debug.Log("start spawn enemies");

        }

        public void StopSpawn()
        {
            UnityEngine.Debug.Log("start spawn enemies");
            
        }
    }
}