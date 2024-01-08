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
        }
    }
}
