using Cinemachine;
using Game.Runtime.Core.Enemies;
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
        [Space, SerializeField] private Transform _playerSpawnPoint;
        [Space, SerializeField] private PCHud _pcHud;  
        [SerializeField] private MobileHud _mobileHud;  
                

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(_mainConfig);
            
            builder.Register<ISceneService, SceneService>(Lifetime.Scoped);
            builder.Register<IObjectResolver, Container>(Lifetime.Singleton);
            builder.Register<IInputService, DeviceInput>(Lifetime.Singleton);

            builder.RegisterComponentInHierarchy<CinemachineVirtualCamera>()
                .AsSelf();

            builder.Register(container => container.Instantiate(_mainConfig.PlayerPrefab, _playerSpawnPoint), Lifetime.Singleton)
                .AsImplementedInterfaces()
                .AsSelf();

            builder.Register<EnemySpawnController>(Lifetime.Singleton)
                .AsImplementedInterfaces()
                .AsSelf();

            RegisterHUD(builder);
        }       

        private void RegisterHUD(IContainerBuilder builder)
        {
            builder.RegisterComponentInHierarchy<MainHud>()
                .AsImplementedInterfaces()
                .AsSelf();


        #if UNITY_EDITOR || UNITY_STANDALONE

            _mobileHud.Hide();

            builder.RegisterComponent(_pcHud)
                .AsImplementedInterfaces()
                .AsSelf();
        
        #elif UNITY_ANDROID

            _defaultHud.Hide();

            builder.RegisterComponent(_mobileHud)
                .AsImplementedInterfaces()
                .AsSelf();
            
        #endif
        }
    }
}
