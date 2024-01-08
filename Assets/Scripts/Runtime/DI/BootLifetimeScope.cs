using VContainer;
using VContainer.Unity;
using Game.Runtime.Services;

namespace Game
{
    public class BootLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<ISceneService, SceneService>(Lifetime.Singleton);
        }
    }
}