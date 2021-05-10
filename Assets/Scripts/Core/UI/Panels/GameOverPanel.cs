using Core.Logic.Scene;
using UnityEngine;
using Zenject;

namespace Core.UI.Panels
{
    public class GameOverPanel : MonoBehaviour
    {
        private ISceneController _sceneController;

        [Inject]
        private void Init(ISceneController sceneController)
        {
            _sceneController = sceneController;
            gameObject.SetActive(false);

            _sceneController.GameFinished += OnGameFinished;
        }

        private void OnDestroy()
        {
            _sceneController.GameFinished -= OnGameFinished;
        }

        private void OnGameFinished()
        {
            Debug.Log("Game is finished!");
            
            gameObject.SetActive(true);
        }
    }
}