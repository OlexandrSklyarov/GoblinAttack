using Cysharp.Threading.Tasks;

namespace Game.Runtime.Services
{
    public interface ISceneService
    {
        UniTask LoadSceneAsync(string sceneName);
        UniTask LoadSceneAddictiveAsync(string sceneName);
        UniTask UpLoadSceneAsync(string sceneName);
    }
}