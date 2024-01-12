using System;
using Cinemachine;
using Game.Runtime.Core.Enemies;
using Game.Runtime.Core.UI;
using Game.Runtime.Data.Configs;
using Game.Runtime.Services.Factories;
using Game.Runtime.Services.Input;
using Game.Runtime.Services.Scenes;
using SA.Runtime.Core.Services.Factories;
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
            builder.Register<IUnitFactory, UnitFactory>(Lifetime.Singleton);

            RegisterInput(builder);

            builder.RegisterComponentInHierarchy<CinemachineVirtualCamera>()
                .AsSelf();

            //enemy controller
            builder.Register<EnemySpawnController>(Lifetime.Singleton)
                .AsImplementedInterfaces()
                .AsSelf();

            //player
            builder.Register(container => container.Instantiate(_mainConfig.PlayerPrefab, _playerSpawnPoint), Lifetime.Singleton)
                .AsImplementedInterfaces()
                .AsSelf();

            RegisterHUD(builder);
        }

        private void RegisterInput(IContainerBuilder builder)
        {            
            //mobile or game
        #if UNITY_EDITOR || UNITY_ANDROID

            builder.Register<IInputService, DeviceInput>(Lifetime.Singleton);        
        
        //pc interface
        #elif UNITY_STANDALONE

            builder.Register<IInputService, KeyboardAndMouseInput>(Lifetime.Singleton);     
                    
        #endif  
        }

        private void RegisterHUD(IContainerBuilder builder)
        {
            builder.RegisterComponentInHierarchy<MainHud>()
                .AsImplementedInterfaces()
                .AsSelf();

            //mobile interface
        #if UNITY_EDITOR || UNITY_ANDROID

            _pcHud.Hide();

            builder.RegisterComponent(_mobileHud)
                .AsImplementedInterfaces()
                .AsSelf();        
        
            //pc interface
        #elif UNITY_STANDALONE

            _mobileHud.Hide();

            builder.RegisterComponent(_pcHud)
                .AsImplementedInterfaces()
                .AsSelf();        
                    
        #endif            
        }
    }
}
