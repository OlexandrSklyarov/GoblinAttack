using Game.Runtime.Services.Scenes;
using GameRuntime.Data;
using UnityEngine;
using VContainer;

namespace Game.Boot
{
    public class AppStartup : MonoBehaviour
    {
        private ISceneService _sceneService;

        [Inject]
        private void Construct(ISceneService sceneService)
        {
            _sceneService = sceneService;
            Debug.Log("Construct");
        }

        private void Start()
        {
            _sceneService.LoadSceneAsync(GameConstants.Scene.GAME);
        }
    }
}
