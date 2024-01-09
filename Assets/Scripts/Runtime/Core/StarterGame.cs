using Cinemachine;
using Game.Runtime.Data.Configs;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Game.Runtime.Core
{
    public class StarterGame : MonoBehaviour
    {
        [SerializeField] private Transform _playerSpawnPoint;

        private IObjectResolver _resolver;
        private MainConfig _mainConfig;
        private CinemachineVirtualCamera _camera;

        [Inject]
        private void Construct(
            IObjectResolver resolver, 
            MainConfig mainConfig,
            CinemachineVirtualCamera camera)
        {
            _resolver = resolver;
            _mainConfig = mainConfig;
            _camera = camera;
        }

        private void Start() 
        {
            var player = _resolver.Instantiate(_mainConfig.PlayerPrefab, _playerSpawnPoint);     

            _camera.Follow = player.TargetPoint;
            _camera.LookAt = player.TargetPoint;
        }
    }
}
