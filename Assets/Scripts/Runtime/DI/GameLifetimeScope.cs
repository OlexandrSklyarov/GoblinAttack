using Cinemachine;
using Game.Runtime.Core.Player;
using Game.Runtime.Core.UI;
using Game.Runtime.Data.Configs;
using Game.Runtime.Services;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Game.Runtime.DI
{
    public class GameLifetimeScope : LifetimeScope
    {
        [Space, SerializeField] private MainConfig _mainConfig;  
        [SerializeField] private Transform _playerSpawnPoint;
        [Space, SerializeField] private Hud _defaultHud;  
        [SerializeField] private MobileHud _mobileHud;  
                

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(_mainConfig);

            builder.Register(container => container.Instantiate(_mainConfig.PlayerPrefab, _playerSpawnPoint), Lifetime.Singleton)
                .AsImplementedInterfaces()
                .AsSelf();

            builder.Register<ISceneService, SceneService>(Lifetime.Singleton);
            builder.Register<IObjectResolver, Container>(Lifetime.Singleton);

            builder.RegisterComponentInHierarchy<CinemachineVirtualCamera>().AsSelf();

            builder.Register<IInputService, DeviceInput>(Lifetime.Singleton);
            
            RegisterHUD(builder);
        }

        private void RegisterHUD(IContainerBuilder builder)
        {
        #if UNITY_EDITOR || UNITY_STANDALONE

            _mobileHud.Hide();
            builder.RegisterComponent(_defaultHud);
        
        #elif UNITY_ANDROID

            _defaultHud.Hide();
            builder.RegisterComponent(_mobileHud);
            
        #endif
        }
    }
}
