using Core.Logic.GameInput;
using Core.Logic.Scene;
using Core.Views;
using UnityEngine;
using Zenject;

namespace Core.Logic.Player
{
	public class PlayerController : MonoBehaviour
	{
		private ISceneController _sceneController;
		private IInputController _inputController;
		private IPlayerFactory _playerFactory;
		private PlayerView _playerView;
		private HealthComponent _playerHealthComponent;
		
		[Inject]
		public void Init(IPlayerFactory playerFactory, ISceneController sceneController, IInputController inputController)
		{
			_playerFactory = playerFactory;
			_sceneController = sceneController;
			_inputController = inputController;
			
			_sceneController.GameStarted += OnGameStarted;
			_inputController.MoveAxisStateChanged += OnMoveAxisStateChanged;
			_inputController.RotateAxisStateChanged += OnRotateAxisStateChanged;
			_inputController.GunChanged += OnGunChanged;
			_inputController.FireStateChanged += OnFireStateChanged;
		}
		
		private void OnGameStarted()
		{
			_playerView = _playerFactory.CreatePlayer();
			_playerHealthComponent = _playerView.HealthComponent; 
			_playerHealthComponent.HealthChanged += OnPlayerHealthChanged;
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

		private void OnGunChanged(int direction)
		{
			if (!_sceneController.IsGameStarted)
				return;
			
			if (direction > 0)
				_playerView.SetNextGun();
			
			if (direction < 0)
				_playerView.SetPreviousGun();
		}

		private void OnFireStateChanged(bool isFireActive)
		{
			_playerView.SetFireState(isFireActive);
		}

		private void OnPlayerHealthChanged(int healthValue)
		{
			if (healthValue > 0)
				return;
			
			_playerView.gameObject.SetActive(false);
			_sceneController.FinishGame();
		}

		private void OnDestroy()
		{
			_sceneController.GameStarted -= OnGameStarted;
			_inputController.MoveAxisStateChanged -= OnMoveAxisStateChanged;
			_inputController.RotateAxisStateChanged -= OnRotateAxisStateChanged;
			_inputController.GunChanged -= OnGunChanged;
			_inputController.FireStateChanged -= OnFireStateChanged;
			
			if (_playerHealthComponent != null)
				_playerHealthComponent.HealthChanged -= OnPlayerHealthChanged;
		}
	}
}