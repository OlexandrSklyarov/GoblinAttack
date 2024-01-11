using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using Game.Runtime.Core.Damage;
using Game.Runtime.Data.Configs;
using Game.Runtime.Util.Extensions;
using UnityEngine;
using VContainer.Unity;

namespace Game.Runtime.Core.Enemies
{
    public class EnemySpawnController : IDisposable, ITickable, IFixedTickable,
        IEnemyWaveInfo, IEnemyKillInfo
    {
        private Vector3 Origin => _player.Position;

        private readonly MainConfig _config;
        private readonly IPlayerDamageTarget _player;
        private readonly List<EnemyUnit> _units = new();
        private bool _isCanUpdateUnits;
        private CancellationTokenSource _cts;
        private int _waveIndex;

        public event Action AllUnitsKillEvent;
        public event Action<int, int> ChangeWaveProgressEvent;
        public event Action<int> EnemyKilledEvent;

        public EnemySpawnController(MainConfig config, IPlayerDamageTarget player)
        {
            _config = config;
            _player = player;
        }        

        public async void StartSpawnAsync()
        {
            _isCanUpdateUnits = true;

            _cts = new CancellationTokenSource();
            var token = _cts.Token;

            while(_waveIndex < _config.EnemyManager.Waves.Length)
            {
                ChangeWaveProgressEvent?.Invoke(_waveIndex + 1, _config.EnemyManager.Waves.Length);

                await SpawnUnitsFromWaveAsync(_config.EnemyManager.Waves[_waveIndex], token);

                _waveIndex++;

                await UniTask.WaitUntil(() => _units.Count <= 0 || token.IsCancellationRequested);

                if (token.IsCancellationRequested) return;
            }

            _isCanUpdateUnits = false;
            
            AllUnitsKillEvent?.Invoke();
        }

        private async UniTask SpawnUnitsFromWaveAsync(EnemyWave wave, CancellationToken token)
        {
            for (int i = 0; i < wave.MaxCount; i++)
            {
                var pos = await GetRandomPositionAsync(Origin, token);

                if (token.IsCancellationRequested) return;

                var prefab = wave.EnemyPrefabs.RandomElement();

                var enemy = GetUnit(prefab, pos);

                enemy.Init(_player);

                enemy.OnDieEvent += OnUnitDie;

                _units.Add(enemy);
            }
        }

        private void OnUnitDie(EnemyUnit unit)
        {
            unit.OnDieEvent -= OnUnitDie;
            _units.Remove(unit);

            EnemyKilledEvent?.Invoke(unit.RewardPoints);
        }

        private EnemyUnit GetUnit(EnemyUnit prefab, Vector3 pos)
        {
            return UnityEngine.Object.Instantiate(prefab, pos, Quaternion.identity);
        }

        private async UniTask<Vector3> GetRandomPositionAsync(Vector3 origin, CancellationToken token)
        {
            var rndDir = UnityEngine.Random.insideUnitSphere * _config.EnemyManager.SpawnRadius.Max;
            rndDir.y = 0f;

            if (rndDir.magnitude < _config.EnemyManager.SpawnRadius.Min) 
            {
                rndDir = rndDir.normalized * _config.EnemyManager.SpawnRadius.Min;
            }
            
            var spawnPos = origin + rndDir;
            
            if (!UnityEngine.AI.NavMesh.SamplePosition(spawnPos, out var hit, 
                _config.EnemyManager.SpawnRadius.Max * 2f, 1))
            {
                await UniTask.DelayFrame(1, cancellationToken: token);

                if (token.IsCancellationRequested) return default;
            }

            spawnPos = hit.position;

            return spawnPos;
        }

        public void StopSpawn()
        {
            CancelSpawn();

            foreach (var unit in _units.ToList())
            {
                unit?.Stop();
            }

            _units.Clear();
        }

        private void CancelSpawn()
        {
            _isCanUpdateUnits = false;

            _cts?.Cancel();
            _cts?.Dispose();
            _cts = null;
        }

        void ITickable.Tick()
        {
            if (!_isCanUpdateUnits) return;

            foreach(var unit in _units.ToList())
            {
                unit?.OnUpdate();
            }
        }

        void IFixedTickable.FixedTick()
        {
            if (!_isCanUpdateUnits) return;

            foreach(var unit in _units.ToList())
            {
                unit?.OnFixedUpdate();
            }
        }

        void IDisposable.Dispose()
        {
            CancelSpawn();
        }
    }
}