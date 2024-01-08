using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace Game.Runtime.Services
{
    public class SceneService : ISceneService
    {
        async UniTask ISceneService.LoadSceneAddictiveAsync(string sceneName)
        {
            await SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        }

        async UniTask ISceneService.LoadSceneAsync(string sceneName)
        {
            await SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
        }

        async UniTask ISceneService.UpLoadSceneAsync(string sceneName)
        {
            await SceneManager.LoadSceneAsync(sceneName);
        }
    }
}
