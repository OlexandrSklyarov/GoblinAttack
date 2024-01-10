using Cinemachine;
using Game.Runtime.Core.Player;
using Game.Runtime.Data.Configs;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Game.Runtime.Core
{
    public class StarterGame : MonoBehaviour
    {
        private PlayerController _player;
        private MainConfig _mainConfig;
        private CinemachineVirtualCamera _camera;

        [Inject()]
        private void Construct(
            PlayerController player, 
            MainConfig mainConfig,
            CinemachineVirtualCamera camera)
        {
            _player = player;
            _mainConfig = mainConfig;
            _camera = camera;
        }

        private void Start() 
        {
            _camera.Follow = _player.TargetPoint;
            _camera.LookAt = _player.TargetPoint;
        }
    }
}
