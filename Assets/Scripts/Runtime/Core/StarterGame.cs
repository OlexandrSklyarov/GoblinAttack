using System;
using Cinemachine;
using Game.Runtime.Core.Enemies;
using Game.Runtime.Core.Player;
using Game.Runtime.Core.UI;
using Game.Runtime.Data.Configs;
using UnityEngine;
using VContainer;

namespace Game.Runtime.Core
{
    public class StarterGame : MonoBehaviour
    {
        private PlayerController _player;
        private MainConfig _mainConfig;
        private CinemachineVirtualCamera _camera;
        private EnemySpawnController _enemySpawnController;
        private IMainHUD _mainHud;

        public event Action GameWinEvent;
        public event Action GameLossEvent;

        [Inject()]
        private void Construct(
            PlayerController player, 
            MainConfig mainConfig,
            CinemachineVirtualCamera camera,
            EnemySpawnController enemySpawnController,
            IMainHUD mainHud)
        {
            _player = player;
            _mainConfig = mainConfig;
            _camera = camera;
            _enemySpawnController = enemySpawnController;
            _mainHud = mainHud;
        }

        private void Start() 
        {
            _mainHud.Hide();

            _camera.Follow = _player.TargetPoint;
            _camera.LookAt = _player.TargetPoint;

            _player.OnDieEvent += OnLoss;

            _enemySpawnController.StartSpawn();
            _enemySpawnController.AllUnitsKillEvent += OnWin;
        }

        private void OnWin()
        {
            _enemySpawnController.AllUnitsKillEvent -= OnWin;
            _player.OnDieEvent -= OnLoss;

            _mainHud.ShowWinMsg();

            GameWinEvent?.Invoke();
        }

        private void OnLoss()
        {
            _enemySpawnController.AllUnitsKillEvent -= OnWin;
            _player.OnDieEvent -= OnLoss;

            _mainHud.ShowLossMsg();

            _enemySpawnController.StopSpawn();
            GameLossEvent?.Invoke();
        }
    }
}
