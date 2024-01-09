using Cinemachine;
using Game.Runtime.Data.Configs;
using Game.Runtime.Services;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Game
{
    public class GameLifetimeScope : LifetimeScope
    {
        [Space, SerializeField] private MainConfig _mainConfig;  
        

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(_mainConfig);

            builder.Register<ISceneService, SceneService>(Lifetime.Singleton);
            builder.Register<IObjectResolver, Container>(Lifetime.Scoped);

            builder.RegisterComponentInHierarchy<CinemachineVirtualCamera>().AsSelf();

            RegisterInput(builder);
        }

        private void RegisterInput(IContainerBuilder builder)
        {
        #if UNITY_EDITOR

            var mobileInput = FindObjectOfType<MobileInput>(true);
            if (mobileInput != null) mobileInput.Hide();

            builder.Register<IInputService, DeviceInput>(Lifetime.Singleton);
        
        #elif UNITY_ANDROID

            builder.RegisterComponentInHierarchy<MobileInput>().AsImplementedInterfaces();

        #endif
        }
    }
}
