using Core.Logic.Scene;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Core.UI.Panels
{
    public class StartPanel : MonoBehaviour
    {
        [SerializeField] private Button _startButton;

        private ISceneController _sceneController;

        [Inject]
        private void Init(ISceneController sceneController)
        {
            _sceneController = sceneController;
        }

        private void Awake()
        {
            _startButton
                .onClick
                .AddListener(OnStartButtonClicked);
        }

        private void OnDestroy()
        {
            _startButton
                .onClick
                .RemoveListener(OnStartButtonClicked);
        }

        private void OnStartButtonClicked()
        {
            Debug.Log("Game is started!");
            
            gameObject.SetActive(false);
            _sceneController.StartGame();
        }
    }
}