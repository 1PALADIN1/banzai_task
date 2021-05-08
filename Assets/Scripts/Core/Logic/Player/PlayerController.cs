using Core.Logic.GameInput;
using Core.Logic.Scene;
using Core.Views;
using UnityEngine;
using Zenject;

namespace Core.Logic.Player
{
	public sealed class PlayerController : MonoBehaviour
	{
		private ISceneController _sceneController;
		private IInputController _inputController;
		private IPlayerFactory _playerFactory;
		private PlayerView _playerView;
		
		[Inject]
		public void Init(IPlayerFactory playerFactory, ISceneController sceneController, IInputController inputController)
		{
			_playerFactory = playerFactory;
			_sceneController = sceneController;
			_inputController = inputController;
			
			_sceneController.GameStarted += OnGameStarted;
			_inputController.MoveAxisStateChanged += OnMoveAxisStateChanged;
			_inputController.RotateAxisStateChanged += OnRotateAxisStateChanged;
		}
		
		private void OnGameStarted()
		{
			_playerView = _playerFactory.CreatePlayer();
		}

		private void OnMoveAxisStateChanged(Vector2 axis)
		{
			if (!_sceneController.IsGameStarted)
				return;
			
			_playerView.SetMoveDirection(axis);
		}


		private void OnRotateAxisStateChanged(Vector2 axis)
		{
			if (!_sceneController.IsGameStarted)
				return;
			
			_playerView.SetRotateDirection(axis);
		}

		private void OnDestroy()
		{
			_sceneController.GameStarted -= OnGameStarted;
			_inputController.MoveAxisStateChanged -= OnMoveAxisStateChanged;
			_inputController.RotateAxisStateChanged -= OnRotateAxisStateChanged;
		}
	}
}